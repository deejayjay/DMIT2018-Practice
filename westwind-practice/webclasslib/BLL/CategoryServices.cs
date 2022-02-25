using DAL;
using ViewModels;

namespace BLL
{
	public class CategoryServices
	{
		#region Constructor Dependency Injection

		private readonly Context _context;
		public CategoryServices(Context context)
		{
			if (context == null)
			{
				throw new ArgumentNullException();
			}
			_context = context;
		}

        #endregion

        #region Queries

		public List<SelectionList> ListCategories()
		{
			return _context.Categories
				.Select(x => new SelectionList
				{
					ValueField = x.CategoryId,
					DisplayField = x.CategoryName
				})
				.OrderBy(x => x.DisplayField)
				.ToList();
		}

        #endregion
    }
}
