using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Advanced.Console.Async
{
    internal class DemoMethods
    {

        //Async method to be awaited
        public static Task<string> DoAsyncResult(string item)
        {
            Task.Delay(1000);
            return Task.FromResult(item);
        }

        //Method to iterate over collection and await DoAsyncResult
        public static async Task<IEnumerable<string>> LoopAsyncResult(IEnumerable<string> thingsToLoop)
        {
            List<Task<string>> listOfTasks = new List<Task<string>>();

            foreach (var thing in thingsToLoop)
            {
                listOfTasks.Add(DoAsyncResult(thing));
            }

            return await Task.WhenAll<string>(listOfTasks);
        }
        public static List<string> PrepData()
        {
            List<string> output = new List<string>();

            output.Add("https://www.yahoo.com");
            output.Add("https://www.google.com");
            output.Add("https://www.microsoft.com");
            output.Add("https://www.cnn.com");
            output.Add("https://www.amazon.com");
            output.Add("https://www.facebook.com");
            output.Add("https://www.twitter.com");
            output.Add("https://www.codeproject.com");
            output.Add("https://www.stackoverflow.com");
            output.Add("https://en.wikipedia.org/wiki/.NET_Framework");

            return output;
        }

        // synchronous method
        // blocks UI Thread
        public static List<WebsiteDataModel> RunDownloadSync()
        {
            List<string> websites = PrepData();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();

            foreach (string site in websites)
            {
                WebsiteDataModel results = DownloadWebsite(site);
                output.Add(results);
            }

            return output;
        }
        // does not block UI thread
        // extra context switching
        // sites are downloaded sequentially
        public async static Task<List<WebsiteDataModel>> RunDownloadAsync()
        {
            List<string> websites = PrepData();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();

            foreach (string site in websites)
            {
                WebsiteDataModel results = await DownloadWebsiteAsync(site);
                output.Add(results);
            }

            return output;
        }
        // does not block UI thread
        // less context switching
        // sites are downloaded parallel and concurrent
        public async static Task<List<WebsiteDataModel>> RunDownloadParallelAsync()
        {
            List<string> websites = PrepData();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();
            foreach (string site in websites)
            {
                tasks.Add(DownloadWebsiteAsync(site));
            }
            var results = await Task.WhenAll(tasks);
            return new List<WebsiteDataModel>(results);
        }
        //public static async IAsyncEnumerable<List<WebsiteDataModel>> RunDownloadParallelv2Async()
        //{
        //    List<string> websites = PrepData();
        //    List<Task> tasks = new List<Task>();
        //    foreach (string site in websites)
        //    {
        //        tasks.Add(DownloadWebsitev2Async(site));
        //    }
        //    //await foreach (var website in Task.WhenAny(tasks.ToArray()))
        //    //{
        //    //    yield return await website.ConfigureAwait(false);
        //    //}

        //}
        public static async IAsyncEnumerable<WebsiteDataModel> RunDownloadParallelv3Async()
        {
            List<string> websites = PrepData();
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();
            foreach (string site in websites)
            {
                tasks.Add(DownloadWebsitev3Async(site));
            }
            while (tasks.Count > 0)
            {
                // Wait for any task to complete
                Task<WebsiteDataModel> completedTask = await Task.WhenAny<WebsiteDataModel>(tasks);
                tasks.Remove(completedTask);

                // Yield the result of the completed task
                yield return await completedTask;
            }

        }
        private static Task DownloadWebsitev2Async(string websiteURL)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            return client.DownloadStringTaskAsync(websiteURL);
        }
        private static Task<WebsiteDataModel> DownloadWebsitev3Async(string websiteURL)
        {
            return Task.Run(async () =>
            {
                WebsiteDataModel output = new WebsiteDataModel();
                using (WebClient client = new WebClient())
                {
                    output.WebsiteUrl = websiteURL;
                    output.WebsiteData = await client.DownloadStringTaskAsync(websiteURL);
                }
                return output;
            });
        }
        private static WebsiteDataModel DownloadWebsite(string websiteURL)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.WebsiteUrl = websiteURL;
            output.WebsiteData = client.DownloadString(websiteURL);

            return output;
        }

        private async static Task<WebsiteDataModel> DownloadWebsiteAsync(string websiteURL)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.WebsiteUrl = websiteURL;
            output.WebsiteData = await client.DownloadStringTaskAsync(new Uri(websiteURL));

            return output;
        }
    }
    public class WebsiteDataModel
    {
        public string WebsiteUrl { get; set; } = "";
        public string WebsiteData { get; set; } = "";
    }
}
