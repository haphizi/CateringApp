using LolaKitchen.Data;
using LolaKitchen.Models;
using LolaKitchen.Utility;
using LolaKitchen.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LolaKitchen.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _db;
        //dependency injection for ihosting for uploading images to the server
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        private MenuItemViewModel MenuItemVM { get; set; }

        public MenuController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            MenuItemVM = new MenuItemViewModel()
            {

                MenuItem = new Models.MenuItem()
            };
        }
        public async Task<IActionResult> Index()
        {
            var menuItems = await _db.MenuItems.ToListAsync();
            return View(menuItems);
        }

        //GET - Create
        public IActionResult Create()
        {


            var categoryList = (from category in _db.Categories
                                where category.ParentCategoryID == null
                                select new SelectListItem()
                                {

                                    Text = category.CategoryName,

                                    Value = category.CategoryID.ToString(),



                                })

                                .ToList();



            categoryList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });

            var subCategoryList = (from category in _db.Categories
                                   where category.ParentCategoryID != null
                                   select new SelectListItem()
                                   {

                                       Text = category.CategoryName,

                                       Value = category.CategoryID.ToString(),



                                   })

                                .ToList();



            subCategoryList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });

            MenuItemVM.CategoryList = categoryList;
            MenuItemVM.SubCategoryList = subCategoryList;

            return View(MenuItemVM);


        }


        //POST - Create


        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {



            MenuItemVM.MenuItem.CategoryID = Convert.ToInt32(Request.Form["CategoryName"].ToString());
            if (!ModelState.IsValid)
            {
                return View(MenuItemVM);
            }

            _db.MenuItems.Add(MenuItemVM.MenuItem);
            await _db.SaveChangesAsync();

            // Work on the image saving section

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var menuItemFromDb = await _db.MenuItems.FindAsync(MenuItemVM.MenuItem.Id);


            if (files.Count > 0)
            {
                //files has been uploaded
                var uploads = Path.Combine(webRootPath, "img");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(uploads, MenuItemVM.MenuItem.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                menuItemFromDb.Image = @"\img\" + MenuItemVM.MenuItem.Id + extension;

            }
            else
            {
                //no file was uploaded
                var uploads = Path.Combine(webRootPath, @"img\" + SD.DefaultFoodImage);
                System.IO.File.Copy(uploads, webRootPath + @"\img\" + MenuItemVM.MenuItem.Id + ".png");
                menuItemFromDb.Image = @"\img\" + MenuItemVM.MenuItem.Id + ".png";
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }   
        
}







 }

