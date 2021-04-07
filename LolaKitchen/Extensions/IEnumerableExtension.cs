//using Microsoft.AspNetCore.Mvc.Rendering;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace LolaKitchen.Extensions
//{
//    public static class IEnumerableExtension
//    {
//        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, int selectedValue)
//        {
//            return from item in items
//                   select new SelectListItem
//                   {
//                       Text = item.GetPropertyValue("CategoryName"),
//                       Value = item.GetPropertyValue("CategoryID"),
//                       Selected = item.GetPropertyValue("CategoryID").Equals(selectedValue.ToString())
//                   };
//        }
//    }
//}
