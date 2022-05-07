using Microsoft.AspNetCore.Http;
using System;

namespace Web_App.Models
{
    public class ProfileItemsVM
    {
        public Guid Id { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile File { get; set; }

    }
}
