﻿using AcunMedyaAkademiWebAPI.Context;
using AcunMedyaAkademiWebAPI.DTOs.CategoriesDto;
using AcunMedyaAkademiWebAPI.DTOs.ProductsDto;
using AcunMedyaAkademiWebAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace AcunMedyaAkademiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly WebAPIDbContext _context;

        public ProductsController(WebAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            var product = _context.Products.ToList();
            return Ok(product); //200 ok
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _context.Products.Find(id);
            if (category == null)
                return NotFound(); //404 NOt Found

            return Ok(category); //200 ok
        }

        [HttpPost]
        public IActionResult Create(ProductsCreateDto productdto)
        {
            //modeli kontrol ediyor
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = new Product
            {
                ProductName = productdto.ProductName,
                Price = productdto.Price,
                ImageUrl = productdto.ImageUrl,
                CategoryId= productdto.CategoryId 
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return Created("", product); //201
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductsUpdateDto productdto)
        {
            //modeli kontrol ediyor
            var Product = _context.Products.Find(id);
            if (Product == null)
                return NotFound();

            Product.ProductName = productdto.ProductName;
            Product.Price = productdto.Price;
            Product.ImageUrl = productdto.ImageUrl;
            _context.SaveChanges();

            //return NoContent(); //204
            /*return StatusCode(204, new {message="Kategori güncellendi"})*/
            ;  //204 
            return Ok("Ürün başarıyla güncellendi");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //modeli kontrol ediyor
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            return NoContent(); //204
            /*return StatusCode(204, new {message="Kategori güncellendi"})*/
            ;  //204 
            //return Ok("Veri başarıyla silindi");
        }
    }
}