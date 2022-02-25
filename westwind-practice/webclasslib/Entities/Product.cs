using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    [Index(nameof(CategoryId), Name = "CategoriesProducts")]
    [Index(nameof(CategoryId), Name = "CategoryID")]
    [Index(nameof(ProductName), Name = "ProductName")]
    [Index(nameof(SupplierId), Name = "SupplierID")]
    [Index(nameof(SupplierId), Name = "SuppliersProducts")]
    public partial class Product
    {
        public Product()
        {
            ManifestItems = new HashSet<ManifestItem>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        [Column("ProductID")]
        public int ProductId { get; set; }
        [StringLength(40)]
        public string ProductName { get; set; } = null!;
        [Column("SupplierID")]
        public int SupplierId { get; set; }
        [Column("CategoryID")]
        public int CategoryId { get; set; }
        [StringLength(20)]
        public string QuantityPerUnit { get; set; } = null!;
        public short? MinimumOrderQuantity { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
        public int UnitsOnOrder { get; set; }
        public bool Discontinued { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; } = null!;
        [ForeignKey(nameof(SupplierId))]
        [InverseProperty("Products")]
        public virtual Supplier Supplier { get; set; } = null!;
        [InverseProperty(nameof(ManifestItem.Product))]
        public virtual ICollection<ManifestItem> ManifestItems { get; set; }
        [InverseProperty(nameof(OrderDetail.Product))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
