using LolaKitchen.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LolaKitchen.ViewModels
{
    public class MenuItemViewModel : CategoryViewModel
    {
        public MenuItem MenuItem { get; set; }
        
        
    }
}
