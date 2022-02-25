using BLL;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webapp.Pages
{
    public class AboutModel : PageModel
    {
        private readonly DbVersionServices _services;
        public AboutModel(DbVersionServices services)
        {
            _services = services;
        }

        public BuildVersion? BuildVersion { get; set; }

        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
            try
            {
                BuildVersion = _services.GetBuildVersion();
                SuccessMessage = "Database retrieval successful";
            }
            catch (Exception ex)
            {
                ErrorMessage = GetInnerException(ex);
            }
        }

        public string GetInnerException(Exception ex) 
        {
            Exception rootCause = ex;

            while (rootCause.InnerException != null)
            {
                rootCause = rootCause.InnerException;
            }

            return rootCause.Message;
        }
    }
}
