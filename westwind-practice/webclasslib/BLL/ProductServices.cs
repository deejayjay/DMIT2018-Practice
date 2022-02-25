using DAL;
using Entities;
using ViewModels;

namespace BLL
{
	public class ProductServices
    {
        #region Constructor Dependency Injection

        private readonly Context _context;
        public ProductServices(Context context)
        {
            if (context == null)
                throw new ArgumentNullException();
            _context = context;
        }
        #endregion

        #region Queries

        public ProductItem Retrieve(int id)
        {
            return _context.Products
                .Where(p => p.ProductId == id)
                .Select(p => new ProductItem
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    SupplierId = p.SupplierId,
                    CategoryId = p.CategoryId,
                    QuantityPerUnit = p.QuantityPerUnit,
                    MinimumOrderQuantity = p.MinimumOrderQuantity,
                    UnitPrice = p.UnitPrice,
                    UnitsOnOrder = p.UnitsOnOrder,
                    Discontinued = p.Discontinued
                }).FirstOrDefault();
        }

        public List<ProductList> FindProductsByPartialName(string partialName)
        {
            return _context.Products
                .Where(p => p.ProductName.Contains(partialName))
                .Select(p => new ProductList
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Supplier = p.Supplier.CompanyName,
                    Category = p.Category.CategoryName,
                    QuantityPerUnit = p.QuantityPerUnit,
                    MinimumOrderQuantity = p.MinimumOrderQuantity,
                    UnitPrice = p.UnitPrice,
                    UnitsOnOrder = p.UnitsOnOrder,
                    Discontinued = p.Discontinued
                })
                .OrderBy(p => p.ProductName)
                .ToList();
        }

        public List<ProductList> FindProductsByCategory(int? categoryId)
        {
            return _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new ProductList
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Supplier = p.Supplier.CompanyName,
                    Category = p.Category.CategoryName,
                    QuantityPerUnit = p.QuantityPerUnit,
                    MinimumOrderQuantity = p.MinimumOrderQuantity,
                    UnitPrice = p.UnitPrice,
                    UnitsOnOrder = p.UnitsOnOrder,
                    Discontinued = p.Discontinued
                })
                .OrderBy(p => p.ProductName)
                .ToList();
        }

        #endregion

        #region Add, Edit, Delete

        public int Add(ProductItem item)
        {
            var newProduct = new Product()
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                SupplierId = item.SupplierId,
                CategoryId = item.CategoryId,
                QuantityPerUnit = item.QuantityPerUnit,
                MinimumOrderQuantity = item.MinimumOrderQuantity,
                UnitPrice = item.UnitPrice,
                UnitsOnOrder = item.UnitsOnOrder,
                Discontinued = item.Discontinued
            };

            _context.Products.Add(newProduct);
            _context.SaveChanges();

            return newProduct.ProductId;
        }

        public void Edit(ProductItem item)
        {
            var existing = _context.Products.Find(item.ProductId);
            if (existing == null)
                throw new Exception("The Product does not exist!!!");
            
            existing.ProductId = item.ProductId;
            existing.ProductName = item.ProductName;
            existing.SupplierId = item.SupplierId;
            existing.CategoryId = item.CategoryId;
            existing.QuantityPerUnit = item.QuantityPerUnit;
            existing.MinimumOrderQuantity = item.MinimumOrderQuantity;
            existing.UnitPrice = item.UnitPrice;
            existing.UnitsOnOrder = item.UnitsOnOrder;
            existing.Discontinued = item.Discontinued;

            _context.Entry(existing).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(ProductItem item)
		{
            var existing = _context.Products.Find(item.ProductId);
			if (existing == null)
                throw new Exception("The Product does not exist!!!");

            _context.Entry(existing).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
		}

        #endregion
    }
}
