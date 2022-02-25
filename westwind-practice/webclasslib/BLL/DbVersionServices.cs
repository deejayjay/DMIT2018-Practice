using DAL;
using Entities;

namespace BLL
{
	public class DbVersionServices
    {
        private readonly Context _context;
        public DbVersionServices(Context context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
            _context = context;
        }

        public BuildVersion GetBuildVersion()
        {
            var result = _context.BuildVersions.ToList();
            return result.First();
        }
    }
}
