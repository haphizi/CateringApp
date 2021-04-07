using LolaKitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LolaKitchen.ViewModels
{
    public class CategoryViewModel : Category
    {
       

        [Display(Name = "Category Id")]
        override public int CategoryID { get; set; }

        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Name cannot be empty!")]
        [StringLength(50)]
        override public string CategoryName { get; set; }
        
        public List<SelectListItem> CategoryList { get; set; }

        
        public List<SelectListItem> SubCategoryList { get; set; }
        public IEnumerable<CategoryViewModel> ChildCategories { get; set; }



        [TempData]
        public string StatusMessage { get; set; }

        public int Level { get; set; }

    }
}
