﻿namespace AcunMedyaAkademiWebUI.DTOs.Product
{
    public class ResultProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }


        public int CategoryId { get; set; }
    }
}
