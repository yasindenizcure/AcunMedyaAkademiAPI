using AcunMedyaAkademiWebUI.DTOs;
using AcunMedyaAkademiWebUI.DTOs.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AcunMedyaAkademiWebUI.Controllers
{
    public class ProductController : Controller
    {
        //HTTP -> internetteki cihazların birbiriyle konuşması
        //Get -> listeleme veri al.
        //Post -> Veri Ekleme.
        //Put -> Veri Güncelleme.
        //Delete -> Veri silme.
        //Client -> istemci.
        //Api -> farklı sistemlerin birbiriyle veri alışverişi yapmasını sağlar.
        //HttpClient -> HttpClient sınıfı, API'ye HTTP istekleri göndermek ve yanıtları almak için kullanılan bir sınıftır.
        //* Her çağrıda yeni bağlantı açar. Performans sorunu oluşur.

        //IHttpClientFactory -> HttpClient nesnelerini oluşturmak için kullanılan bir fabrikadır. Bu, HttpClient nesnelerinin yönetimini ve yapılandırmasını kolaylaştırır. Merkezden yönetir. Aynı bağlantıları tekrar tekrar kullanılmasını sağlar.
        //* Daha doğru ve performanslı bir şekilde HttpClient nesneleri oluşturulmasını sağlar.

        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(); //httpClient configrasyonu döner clientla ilgili temel özellikleri yapabiliyoruz
            var response = await client.GetAsync("https://localhost:7108/api/Products");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsondata);
                //deserialize -> jsondan text string çevirme
                //serialize -> text'ten json çevirme
                return View(values);
            }
            return View();
        }
        public async Task<IActionResult> GetAllWithCategory()
        {
            var client = _httpClientFactory.CreateClient(); //httpClient configrasyonu döner clientla ilgili temel özellikleri yapabiliyoruz
            var response = await client.GetAsync("https://localhost:7108/api/Products/GetAllWithCategory"); //url
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ProductWithCategoryDto>>(jsondata); //dto
                //deserialize -> jsondan text string çevirme
                //serialize -> text'ten json çevirme
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsondata = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsondata, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7108/api/Products", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7108/api/Products/" + id);
            var jsondata = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsondata);
            return View(values);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsondata = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsondata, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"https://localhost:7108/api/Products/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync("https://localhost:7108/api/Products/" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }

    }
}