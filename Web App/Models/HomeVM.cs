using Data.Entities;
using System.Collections.Generic;

namespace Web_App.Models
{
    public class HomeVM
    {
        public Owner Owner { get; set; }
        public List<ProfileItem> Items { get; set; }
    }
}
