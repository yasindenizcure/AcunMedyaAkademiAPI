﻿namespace AcunMedyaAkademiWebAPI.DTOs.ProductsDto
{
    public class ProductWithCategoryDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
    }
}
