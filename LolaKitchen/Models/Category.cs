using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LolaKitchen.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [Display(Name = "Category Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        virtual public int CategoryID { get; set; }

        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Name cannot be empty!")]
        [MaxLength(50)]
        virtual public string CategoryName { get; set; }

        // Declared NULLable
        [Display(Name = "Parent Category Id")]
        [ForeignKey("ParentCategoryID")]
        public int? ParentCategoryID { get; set; }
    }
}
