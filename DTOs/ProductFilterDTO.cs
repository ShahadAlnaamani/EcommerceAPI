using EcommerceTask.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EcommerceTask.DTOs
{
    public class ProductFilterDTO
    {
        [Range(1, int.MaxValue)]
        [DefaultValue(1)]
        public int Page { get; set; }

        [Range(1, int.MaxValue)]
        [DefaultValue(1000)]
        public int PageSize { get; set; }
        public  string CategoryName { get; set; }

        public string ProductName { get; set; }

        [Range(1,5)]
        public int rating { get; set; }
    }
}
