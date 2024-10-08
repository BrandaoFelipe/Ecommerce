﻿using System.ComponentModel.DataAnnotations;

namespace CartAPI.DTOs
{
    public class CartHeaderDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "UserId is required")]
        public string? UserId { get; set; } = string.Empty;
        public string? CouponCode { get; set; } = string.Empty;
    }
}
