using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels;

namespace webapp.Pages
{
    public class QueryCrudModel : PageModel
    {
        #region Constructor Dependency Injection

        private readonly ProductServices _productServices;
        private readonly SupplierServices _supplierServices;
        private readonly CategoryServices _categoryServices;

        public QueryCrudModel(ProductServices productServices, SupplierServices supplierServices, CategoryServices categoryServices)
        {
            _productServices = productServices;
            _supplierServices = supplierServices;
            _categoryServices = categoryServices;
        }

        #endregion

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public List<Exception> Errors { get; set; } = new();
        [BindProperty]
        public string ButtonPressed { get; set; }
        [BindProperty]
        public string FilterType { get; set; }
        [BindProperty]
        public string PartialProductName { get; set; }
        [BindProperty]
        public int? SelectedCategoryId { get; set; }
        [BindProperty]
        public List<ProductList> SearchedProducts { get; set; }
        [BindProperty]
        public ProductItem Product { get; set; } = new();
        [BindProperty]
        public string Discontinued { get; set; }
        [BindProperty]
        public List<SelectionList> SelectListOfCategories { get; set; }
        [BindProperty]
        public List<SelectionList> SelectListOfSuppliers { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                PopulateSelectLists();
            }
            catch (Exception ex)
            {
                ErrorMessage = GetInnerException(ex);
            }
            return Page();
        }

        public IActionResult OnPost()
		{
			try
			{
				if (ButtonPressed == "SearchByPartialProductName")
				{
                    FilterType = "PartialString";
				}
                else if (ButtonPressed == "SearchByCategory")
				{
                    FilterType = "CategoryId";
                }
				else if (ButtonPressed == "Add")
				{
					Product.Discontinued = (Discontinued == "on");

                    FormValidation();
                    Product.ProductId = _productServices.Add(Product);
                    SuccessMessage = "Add Successful";
				}
                else if (ButtonPressed == "Update")
				{
                    Product.Discontinued = (Discontinued == "on");

                    FormValidation();
                    _productServices.Edit(Product);
                    SuccessMessage = "Update Successful";
				}
                else if (ButtonPressed == "Delete")
                {
                    _productServices.Delete(Product);
                    Product = new ProductItem();
                    SuccessMessage = "Delete Successful";
                }
                else if (ButtonPressed == "Clear")
                {
                    Product = new ProductItem();
                    SuccessMessage = "Clear Successful";
                }
                else if (Product.ProductId != 0)
                {
                    Product = _productServices.Retrieve(Product.ProductId);
                    SuccessMessage = "Product Retrieval Successful...";
                }
                else
				{
                    ErrorMessage = "Danger: Invalid Operation!!!";
				}
            }
            catch(AggregateException ex)
			{
                ErrorMessage = ex.Message;
			}
			catch (Exception ex)
			{
                ErrorMessage = GetInnerException(ex);
			}

            PopulateSelectLists();
            GetProducts(FilterType);
            return Page();
		}

		private void FormValidation()
		{
			if (string.IsNullOrWhiteSpace(Product.ProductName))
			{
                Errors.Add(new Exception("ProductName"));
			}

			if (Product.SupplierId == 0)
			{
                Errors.Add(new Exception("SupplierId"));
            }

            if (Product.CategoryId == 0)
            {
                Errors.Add(new Exception("CategoryId"));
            }

            if (string.IsNullOrWhiteSpace(Product.QuantityPerUnit))
			{
                Errors.Add(new Exception("QuantityPerUnit"));
            }

			if (Errors.Count > 0)
			{
                throw new AggregateException("Invalid Data: ", Errors);
			}
		}

		private void GetProducts(string filterType)
		{
			try
			{
				if (filterType == "PartialString")
				{
                    SearchedProducts = _productServices.FindProductsByPartialName(PartialProductName);
				}
                else if (filterType == "CategoryId")
				{
                    SearchedProducts = _productServices.FindProductsByCategory(SelectedCategoryId);
				}
			}
			catch (Exception ex)
			{
                ErrorMessage = GetInnerException(ex);
			}
		}

		private string GetInnerException(Exception ex)
        {
            Exception rootCause = ex;
            while (rootCause.InnerException != null)
            {
                rootCause = rootCause.InnerException;
            }
            return rootCause.Message;
        }

		private void PopulateSelectLists()
		{
			try
			{
				SelectListOfCategories = _categoryServices.ListCategories();
				SelectListOfSuppliers = _supplierServices.ListSuppliers();
			}
			catch (Exception ex)
			{
				ErrorMessage = GetInnerException(ex);
			}
		}
	}
}
