using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

public class BookController : Controller
{
    private const string GoogleBooksApiBaseUrl = "https://www.googleapis.com/books/v1/volumes";
    private const string ApiKey = "AIzaSyCl3CvyzufqvpDKbjuI8T7F3bijc9MA7cw"; 

    public async Task<ActionResult> Index(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return View(new List<BookViewModel>());
        }

        var books = await SearchBooks(query);
        return View(books);
    }

    private async Task<List<BookViewModel>> SearchBooks(string query)
    {
        using (var httpClient = new HttpClient())
        {
            var apiUrl = $"{GoogleBooksApiBaseUrl}?q={query}&key={ApiKey}";

            try
            {
                var response = await httpClient.GetStringAsync(apiUrl);
                var result = JsonConvert.DeserializeObject<GoogleBooksApiResponse>(response);

                if (result != null && result.Items != null)
                {
                    var books = new List<BookViewModel>();

                    foreach (var item in result.Items)
                    {
                        var book = new BookViewModel
                        {
                            Title = item.VolumeInfo.Title,
                            Authors = item.VolumeInfo.Authors,
                            Description = item.VolumeInfo.Description,
                            ThumbnailUrl = item.VolumeInfo.ImageLinks?.Thumbnail ?? "https://via.placeholder.com/150"
                        };

                        books.Add(book);
                    }

                    return books;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        return new List<BookViewModel>();
    }
}

public class GoogleBooksApiResponse
{
    public List<GoogleBooksApiItem> Items { get; set; }
}

public class GoogleBooksApiItem
{
    public GoogleBooksApiVolumeInfo VolumeInfo { get; set; }
}

public class GoogleBooksApiVolumeInfo
{
    public string Title { get; set; }
    public List<string> Authors { get; set; }
    public string Description { get; set; }
    public GoogleBooksApiImageLinks ImageLinks { get; set; }
}

public class GoogleBooksApiImageLinks
{
    public string Thumbnail { get; set; }
}

public class BookViewModel
{
    public string Title { get; set; }
    public List<string> Authors { get; set; }
    public string Description { get; set; }
    public string ThumbnailUrl { get; set; }
}
