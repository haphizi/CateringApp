using LolaKitchen.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LolaKitchen.Models
{
    public class MenuItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Item Name")]
        [Required(ErrorMessage = "Name cannot be empty!")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description cannot be empty!")]
        [MaxLength(50)]
        public string? Description { get; set; }


        public string Image { get; set; }

        [Display(Name = "Category Id")]
        public int CategoryID { get; set; }

        [Range(1,int.MaxValue, ErrorMessage = "Price shoulder be greather than ${1}")]
        public double Price { get; set; }


        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

    }
}
