using System;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.ProductCatalog.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public double Price { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
