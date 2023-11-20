using Microsoft.AspNetCore.Mvc;

namespace Task1.Controllers
{
    [ApiController]
    [Route("[controller]/api")]
    public class DogController : ControllerBase
    {
        #region private properties
        private readonly HttpClient _httpClient;
        private const string DogImageUrl = "https://dog.ceo/api/breeds/image/random";
        private readonly ILogger<DogController> _logger;
        #endregion private properties

        #region constructure
        public DogController(ILogger<DogController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }
        #endregion constructure

        #region public methods
        [HttpGet("image/random")]
        public async Task<IActionResult> GetRandomImage()
        {
            return Ok(await this.GetDogImageAsync());
        }
        #endregion public methods

        #region private methods
        private async Task<string> GetDogImageAsync()
        {
            _httpClient.BaseAddress = new Uri(DogImageUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string dogImage = string.Empty;

            HttpResponseMessage response = await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                dogImage = await response.Content.ReadAsStringAsync();
            }

            return dogImage;
        }
        #endregion private methods
    }
}
