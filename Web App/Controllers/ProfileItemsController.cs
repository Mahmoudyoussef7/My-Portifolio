using Business.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web_App.Models;

namespace Web_App.Controllers
{
    public class ProfileItemsController : Controller
    {

        private readonly IUnitOfWork<ProfileItem> _profileItem;
        private readonly IHostingEnvironment _hosting;


        public ProfileItemsController(IUnitOfWork<ProfileItem> profileItem, IHostingEnvironment hosting)
        {
            _profileItem = profileItem;
            _hosting = hosting;
        }

        // GET: PortfolioItems
        public IActionResult Index()
        {
            return View(_profileItem.Entity.GetAll());
        }

        // GET: PortfolioItems/Details/5
        public IActionResult Details(Guid? id)
        {   
            var portfolioItem = _profileItem.Entity.GetById(id);
            if (portfolioItem == null || id==null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // GET: PortfolioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProfileItemsVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string fullPath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                ProfileItem portfolioItem = new ProfileItem
                {
                    ItemName = model.ItemName,
                    Description = model.Description,
                    ImageUrl = model.File.FileName,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsActive=true
                   
                };

                _profileItem.Entity.Insert(portfolioItem);
                _profileItem.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _profileItem.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            ProfileItemsVM portfolioViewModel = new ProfileItemsVM
            {
                Id = portfolioItem.Id,
                Description = portfolioItem.Description,
                ImageUrl = portfolioItem.ImageUrl,
                ItemName = portfolioItem.ItemName,
            };

            return View(portfolioViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, ProfileItemsVM model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                        string fullPath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }

                    ProfileItem portfolioItem = new ProfileItem
                    {
                        Id = model.Id,
                        ItemName = model.ItemName,
                        Description = model.Description,
                        ImageUrl = model.File.FileName,
                        UpdatedAt = DateTime.Now,
                        IsActive = true

                    };

                    _profileItem.Entity.Update(portfolioItem);
                    _profileItem.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortfolioItems/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _profileItem.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _profileItem.Entity.Delete(id);
            _profileItem.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(Guid id)
        {
            return _profileItem.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
