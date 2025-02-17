using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//visit the link 
//https://github.com/brminnick/AsyncAwaitBestPractices/blob/main/sample/ViewModels/NewsViewModel_GoodAsyncAwaitPractices.cs


namespace CSharp.Advanced.Console.Async
{
    public class Streaming
    {

        public async Task Refresh()
        {
            var minimumRefreshTimeTask = Task.Delay(TimeSpan.FromSeconds(2));

            try
            {
                await foreach (var book in GetTopStoriesStreaming(2).ConfigureAwait(false))
                {
                    System.Console.WriteLine($"this is book {book.Id} with description: {book.Name}");
                }
            }
            catch (Exception e)
            {
                //OnPullToRefreshFailed(e.ToString());
            }
            finally
            {
                await minimumRefreshTimeTask.ConfigureAwait(false);
                //IsListRefreshing = false;
            }
        }
        public async Task RefreshAsync()
        {
            var getTopBooks = await GetTopStoriesAsync(2);
           
                foreach (var book in getTopBooks)
                {
                    System.Console.WriteLine($"this is book {book.Id} with description: {book.Name}");
                }
            
          
        }

        async IAsyncEnumerable<Book> GetTopStoriesStreaming(int storyCount)
        {
            var bookIds = await Task<IEnumerable<int>>.Run(()=> new List<int> { 2,4,10,3,6,7,11});

            var getBooksTaskList = bookIds.Select(id => GetStory(id)).ToList();

            while (getBooksTaskList.Any())
            {
                // Wait for any task to complete
                var completedTask = await Task.WhenAny(getBooksTaskList);

                // Remove the completed task from the list
                getBooksTaskList.Remove(completedTask);

                // Yield the result
                yield return await completedTask.ConfigureAwait(false);
            }
        }
        async Task<IEnumerable<Book>> GetTopStoriesAsync(int storyCount)
        {
            var bookIds = await Task<IEnumerable<int>>.Run(() => new List<int> { 2, 4, 10, 3, 6, 7, 11 });

            var getBooksTaskList = bookIds.Select(id => GetStory(id)).ToList();
            return await Task.WhenAll(getBooksTaskList).ConfigureAwait(false);
                
        }

        private async Task<Book> GetStory(int id)
        {   
            var randomWait = new Random().Next(1,12);
            await Task.Delay(randomWait * 1000); // Await the delay with cancellation support
            var book = new Book
            {
                Id = id,
                Name = $"book {id}"
            };

            return book;
        }

        

        public static async IAsyncEnumerable<int> YieldReturnNumbers(List<int> numbers) 
        {
            foreach (int number in numbers)
            {   
                await Task.Delay(1000);
                yield return number;
            }
        }


    }

    internal class Book
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
