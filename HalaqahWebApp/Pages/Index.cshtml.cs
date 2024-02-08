using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HalaqahModel.Models;
using Newtonsoft.Json;

namespace HalaqahWebApp.Pages;

public class IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory) : PageModel
{
    public List<Student> Students = [];

    public async Task<IActionResult> OnGetAsync()
    {
        var httpClient = httpClientFactory.CreateClient();
        using (var response = await httpClient.GetAsync("http://localhost:5285/api/Student"))
        {
            if (!response.IsSuccessStatusCode) return Page();
            
            var apiResponse = await response.Content.ReadAsStringAsync();
            Students = JsonConvert.DeserializeObject<List<Student>>(apiResponse) ?? [];
        }

        return Page();
    }
}