using AcunMedyaAkademiWebAPI.Context;
using AcunMedyaAkademiWebAPI.DTOs.CategoriesDto;
using AcunMedyaAkademiWebAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcunMedyaAkademiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly WebAPIDbContext _context;

        public CategoriesController(WebAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var category = _context.Categories.ToList();
            return Ok(category); //200 ok
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)

                return NotFound(); //404 Not Found

            return Ok(category); //200 ok
        }
        [HttpPost]
        public IActionResult Create(CategoriesCreateDto categorydto)
        {
            //modeli kontrol ediyor
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }


            var category = new Category
            {
                CategoryName = categorydto.CategoryName
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
          
            return Created("",category); //201
            //return Ok("Category created successfully");
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,CategoriesUpdateDto categorydto)
        {
            var category = _context.Categories.Find(id);
            if(category == null)
            
                return NotFound();
            
           category.CategoryName = categorydto.CategoryName;
           _context.SaveChanges();

            //return NoContent(); //204
            //return StatusCode(204, new { message = "Kategori Güncellendi." }); //204
            return Ok("Kategori Başarıyla Güncellendi.");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)

                return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return NoContent(); //204
            //return StatusCode(204, new { message = "Kategori Güncellendi." }); //204
            //return Ok("Kategori Başarıyla Güncellendi.");
        }
    }
}
