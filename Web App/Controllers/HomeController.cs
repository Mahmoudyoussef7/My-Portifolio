using Business.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web_App.Models;

namespace Web_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<ProfileItem> _profileItem;
        private readonly IUnitOfWork<Owner> _owner;

        public HomeController(IUnitOfWork<Owner> owner,IUnitOfWork<ProfileItem> profileItem)
        {
            _profileItem = profileItem;
            _owner = owner;
        }

        public IActionResult Index()
        {
            HomeVM model = new HomeVM
            {
                Owner = _owner.Entity.GetAll().FirstOrDefault(),
                Items = _profileItem.Entity.GetAll().ToList()
            };
            return View(model);
        }
        public IActionResult About()
        {
            return View();
        }
        
        public IActionResult Contact()
        {
            return View();
        }
    }
}
