using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreTest.Data.Entities
{
    public class NwmProduct
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int ProductCategoryId { get; set; }
        public int BrandId { get; set; }
        public int RetailerId { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public Boolean MainPage { get; set; }
        public Boolean State { get; set; }
        public int Ordering { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean ImageState { get; set; }
        public DateTime UpdatedDate { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public int UnitsInStock { get; set; }
        public int TotalRating { get; set; }
        public string VideoUrl { get; set; }
    }
}
