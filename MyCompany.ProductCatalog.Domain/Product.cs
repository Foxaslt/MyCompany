using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCompany.ProductCatalog.Domain
{
    public class Product
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [DataType(DataType.Text)]
        [Required]
        [Display(Name = "Code")]
        [Remote(action: "VerifyCode", controller: "Products")]
        public string Code { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [ReadOnly(true)]
        [Editable(false, AllowInitialValue = false)]
        [DisplayName("Last updated")]
        [Timestamp]
        public DateTime LastUpdated { get; set; }
    }
}
