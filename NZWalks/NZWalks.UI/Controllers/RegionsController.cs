using Microsoft.AspNetCore.Mvc;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                //To get all regions data from api
                var client = httpClientFactory.CreateClient();

                var httpresponse = await client.GetAsync("https://localhost:7062/api/regions");
                httpresponse.EnsureSuccessStatusCode();
             var stringresponsebody= await httpresponse.Content.ReadAsStringAsync();
                ViewBag.Response=  stringresponsebody;
            }
            catch(Exception ex)
            {
                throw;
            }

           return View();
        }
    }
}
