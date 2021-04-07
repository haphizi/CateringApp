using LolaKitchen.Data;
using LolaKitchen.Models;
using LolaKitchen.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LolaKitchen.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;




        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
            
        }

        // GET
        public IActionResult Index()
        {

            IEnumerable<CategoryViewModel> modelList
                = GetViewModel(null, 0, _db.Categories.ToList());

            return View("Index", modelList);

        }

        // GET - Create
        public IActionResult Create()
        {
        
            return View();
        }


        // GET - Edit
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var category =  _db.Categories.Find(id);

            if(category == null)
            {
                return NotFound();

            }
            return View(category);
        }

        // GET - Details
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();

            }
            return View(category);
        }


        // GET - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();

            }
            return View(category);
        }

        // GET - Create2 (Subcategory)

        public IActionResult Create2(int CategoryID)
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

           

            CategoryViewModel modelCVM = new CategoryViewModel();

           modelCVM.CategoryList = categoryList;

            //var subCategoryList = await _db.Categories.OrderBy(p => p.CategoryName)
            //                      .Where(p => p.ParentCategoryID == p.CategoryID)
            //                      .Select(p => p.CategoryName)
            //                      .Distinct()
            //                      .ToListAsync();
            //modelCVM.SubCategoryList = subCategoryList;


            return View(modelCVM);
        }

        //GET- for subcategories

       
        public IActionResult GetSubCategory(int CategoryID)
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

           


            CategoryViewModel modelCVM = new CategoryViewModel();

            modelCVM.CategoryList = categoryList;

            return View(modelCVM);

            //CategoryViewModel subCategories = new CategoryViewModel();
            //subCategories = ((CategoryViewModel)(from subCategory in _db.Categories
            //                                     where subCategory.ParentCategoryID == CategoryID
            //                                     select subCategory.CategoryName));



            //return Json(new SelectList((System.Collections.IEnumerable)subCategories, "CategoryName"));
        }





        // POST - CREATE2 for subcategories
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create2(int CategoryID, string ChildCategories)
        {
            // CategoryViewModel StatusMessageVM = new CategoryViewModel();

            if (ModelState.IsValid)
            {
                Category addSubCategory = new Category();
                {

                    if (addSubCategory == null)
                    {
                        return NotFound();

                    }


                    else if (addSubCategory.ParentCategoryID == null)
                    {
                        addSubCategory.CategoryName = ChildCategories;
                        addSubCategory.ParentCategoryID = CategoryID;

                        //ERROR
                        //StatusMessageVM.StatusMessage = "Error : SubCategory exists under"
                        //     + addSubCategory.CategoryName
                        //     + " category. Please use another name.";
                    }
                }

               

            

                await _db.Categories.AddAsync(addSubCategory);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // string statusMessage = StatusMessageVM.StatusMessage;
            return View();
        }

        // POST - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if(ModelState.IsValid)
            {
                _db.Add(category);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        // POST - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                var categoryFound = _db.Categories.Find(id);

                if (categoryFound == null)
                {
                    return NotFound();

                }
                categoryFound.CategoryName = category.CategoryName;
                _db.Update(categoryFound);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            { 
                
                return NotFound();
                 // return View();
            }
            
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private IEnumerable<CategoryViewModel> GetViewModel(
        int? parentId, int level, List<Category> _db)
        {
            return from category in _db
                   where category.ParentCategoryID == parentId
                   select new CategoryViewModel
                   {
                       CategoryID = category.CategoryID,
                       CategoryName = category.CategoryName,
                       ParentCategoryID = parentId,
                       Level = level,
                       ChildCategories
                           = GetViewModel(category.CategoryID, level + 1, _db)
                   };
        }


    }
}
