﻿using System.ComponentModel.DataAnnotations;

namespace WEB.Models;

public class ProductsViewModel
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public long Stock { get; set; }

    [Required]
    public string? ImageURL { get; set; }

    [Display(Name = "Category")]
    public string? CategoryName { get; set; }

    [Display(Name ="Category")]
    public int CategoryId { get; set; }
}
