using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSharp.Advanced.Console.Async
{
    public class YoutubeVideosAPI
    {   
        private static readonly HttpClient _httpClient = new HttpClient();
        private const int TASK_COUNT = 1000;

        public List<int> ParallelVersion() 
        {
            var list  = new List<int>();
            var youtubeSubscriberTasks = Enumerable.Range(0,TASK_COUNT).Select(_ => new Func<int>(() => GetYoutubeSubscribersAsync(_httpClient).GetAwaiter().GetResult())).ToList();
            Parallel.For(0, youtubeSubscriberTasks.Count, i => list.Add(youtubeSubscriberTasks[i]())); // invoking the method here, cz parallel is not async operation
            
            return list;
        
        }
        public async Task<List<int>> ForEachVersion()
        {
            var list = new List<int>(); 
            var youtubeSubscriberTasks = Enumerable.Range(0, TASK_COUNT).Select(_ => new Func<Task<int>>(() => GetYoutubeSubscribersAsync(_httpClient))).ToList();
            foreach (var item in youtubeSubscriberTasks)
            {
                list.Add(await item());
            }

            return list;

        }
        public async Task<List<int>> WhenAllVersion()
        {
            var list = new List<int>();
            var youtubeSubscriberTasks = Enumerable.Range(0, TASK_COUNT).Select(_ =>  GetYoutubeSubscribersAsync(_httpClient)).ToList();
            var results = await Task.WhenAll(youtubeSubscriberTasks);
            return results.ToList();

        }



        private static async Task<int> GetYoutubeSubscribersAsync(HttpClient httpClient) 
        {
            var githubResponse = await _httpClient.GetStringAsync($"https://localhost/youtube");
            var youtubeUser = JsonSerializer.Deserialize<YoutubeUser>(githubResponse);
            return youtubeUser!.Subscribers;
        }
    }

    internal class YoutubeUser
    {
        public int Subscribers { get; set; }
    }
}
