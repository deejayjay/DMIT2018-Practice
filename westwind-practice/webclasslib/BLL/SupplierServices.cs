using DAL;
using ViewModels;

namespace BLL
{
	public class SupplierServices
	{
		#region Constructor Dependency Injection

		private readonly Context _context;
		public SupplierServices(Context context)
		{
			if (context == null)
			{
				throw new ArgumentNullException();
			}
			_context = context;
		}

		#endregion

		#region Queries

		public List<SelectionList> ListSuppliers()
		{
			return _context.Suppliers
				.Select(x => new SelectionList
				{
					ValueField = x.SupplierId,
					DisplayField = x.CompanyName
				})
				.OrderBy(x => x.DisplayField)
				.ToList();
		}

		#endregion
	}
}
