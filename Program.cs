using OptechX.NewsReader.Models;
using OptechX.NewsReader.Models.OptechX.API.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace OptechX.NewsReader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Delete existing news articles
            using (HttpClient client = new HttpClient())
            {
                string baseOptechXNewsUrl = "https://definitely-firm-chamois.ngrok-free.app/api/NewsUpdate";

                // Perform HttpGet to retrieve the list of objects
                HttpResponseMessage getResponse = await client.GetAsync(baseOptechXNewsUrl);
                if (!getResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine($"GET request failed. Status code: {getResponse.StatusCode}");
                    return;
                }

                // Deserialize the response content into a list of objects
                string jsonResponse = await getResponse.Content.ReadAsStringAsync();
                List<OptechXNewsItem> newsItems = JsonSerializer.Deserialize<List<OptechXNewsItem>>(jsonResponse)!;

                // Iterate through each news item and perform HttpDelete
                foreach (OptechXNewsItem newsItem in newsItems)
                {
                    string apiDeleteUrl = $"{baseOptechXNewsUrl}/{newsItem.Id}";
                    HttpResponseMessage deleteResponse = await client.DeleteAsync(apiDeleteUrl);

                    if (deleteResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"DELETE request for {apiDeleteUrl} was successful.");
                    }
                    else
                    {
                        Console.WriteLine($"DELETE request for {apiDeleteUrl} failed. Status code: {deleteResponse.StatusCode}");
                    }
                }
            }

            // Parse command line arguments
            string apiKey = args[Array.IndexOf(args, "--apiKey") + 1];
            string country = args[Array.IndexOf(args, "--country") + 1];
            string category = args[Array.IndexOf(args, "--category") + 1];
            int pageSize = int.Parse(args[Array.IndexOf(args, "--pageSize") + 1]);

            // Construct API URL
            string apiUrl = $"https://newsapi.org/v2/top-headlines?apiKey={apiKey}&country={country}&category={category}&pageSize={pageSize}";

            Console.WriteLine(apiUrl);

            // Memory storage for Articles
            List<SavedArticle> savedArticles = new();

            // Make HTTP GET request
            using (HttpClient client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                // Set headers
                client.DefaultRequestHeaders.UserAgent.ParseAdd("OptechX News Tool");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Connection.Add("keep-alive");

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    //Console.WriteLine(jsonResponse);
                    TopHeadline topHeadline = JsonSerializer.Deserialize<TopHeadline>(jsonResponse)!;

                    // Access articles
                    List<Article>? articles = topHeadline.Articles;
                    if (articles != null)
                    {
                        foreach (Article article in articles)
                        {
                            if (!string.IsNullOrEmpty(article.Title) &&
                                !string.IsNullOrEmpty(article.Description) &&
                                !string.IsNullOrEmpty(article.UrlToImage) &&
                                !string.IsNullOrEmpty(article.Url))
                            {
                                SavedArticle savedArticle = new()
                                {
                                    Title = article.Title,
                                    Description = article.Description,
                                    UrlToImage = article.UrlToImage,
                                    Url = article.Url,
                                };
                                savedArticles.Add(savedArticle);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }

            // Shuffle the list randomly
            Random random = new Random();
            List<SavedArticle> shuffledArticles = savedArticles.OrderBy(item => random.Next()).ToList();

            // Select the first 4 items as random items
            List<SavedArticle> randomItems = shuffledArticles.Take(4).ToList();

            // Print the selected random items
            using (HttpClient client = new HttpClient())
            {
                foreach (SavedArticle article in randomItems)
                {
                    string json = JsonSerializer.Serialize(article);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("https://definitely-firm-chamois.ngrok-free.app/api/NewsUpdate", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Article '{article.Title}' sent successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Error sending article '{article.Title}'. Status code: {response.StatusCode}");
                    }
                }
            }
        }
    }
}