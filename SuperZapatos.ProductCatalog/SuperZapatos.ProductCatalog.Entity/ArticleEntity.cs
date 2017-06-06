using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SuperZapatos.ProductCatalog.Entity
{
    public class ArticleEntity
    {
        [Display(Name = "Article ID")]
        public int ArticleId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(150)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(1, 100, ErrorMessage = "Price must be between $1 and $100")]
        public decimal Price { get; set; }
        [Display(Name = "Total in shelf")]
        [Required(ErrorMessage = "Total in shelf is required")]
        public int TotalInShelf { get; set; }
        [Display(Name = "Total in vault")]
        [Required(ErrorMessage = "Total in vault is required")]
        public int TotalInVault { get; set; }
        [Display(Name = "Store")]
        [Required(ErrorMessage = "Store is required")]
        public int StoreId { get; set; }
    }
}
