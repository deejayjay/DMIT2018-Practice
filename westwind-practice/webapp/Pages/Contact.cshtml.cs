using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webapp.Pages
{
    public class ContactModel : PageModel
    {
        public string Text1 { get; set; }

        [BindProperty]
        public string Text2 { get; set; }
        [BindProperty]
        public string Text3 { get; set; }

        public int Number1 { get; set; }

        [BindProperty]
        public int Number2 { get; set; }
        [BindProperty]
        public int Number3 { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public DateTime MyDate { get; set; }

        public List<string> SelectListOfSubjects { get; set; }

        [BindProperty]
        public int SelectedSubjectId { get; set; }

        [BindProperty]
        public string MessageBody { get; set; }

        [BindProperty]
        public string CheckBox { get; set; }

        [BindProperty]
        public string Radio { get; set; }

        [BindProperty]
        public string Range { get; set; }

        [BindProperty]
        public string ButtonPressed { get; set; }

        public string SuccessMessage { get; set; }

        public string ErrorMessage { get; set; }

        public List<Exception> Errors { get; set; } = new();

        public void OnGet()
        {
            PopulateSelectLists();
        }

       public IActionResult OnPost(string text1, string number1)
		{
            try
            {
                PopulateSelectLists();

                Text1 = text1;

                if (!string.IsNullOrWhiteSpace(number1))
                {
                    Number1 = int.Parse(number1);
                }

				if (ButtonPressed == "Submit")
				{
					if (string.IsNullOrWhiteSpace(Text1))
					{
                        Errors.Add(new Exception("Text1"));
					}
					if (SelectedSubjectId == 0)
					{
                        Errors.Add(new Exception("DropDown"));
					}

					if (Errors.Count > 0)
					{
                        throw new AggregateException("Missing Data: ", Errors);
					}

                    SuccessMessage = $"";
				}
            }
            catch (AggregateException e) 
            { 
                ErrorMessage = e.Message;
            }
            catch (Exception e)
            {
                ErrorMessage = GetInnerException(e);
            }

            return Page();
		}

        private void PopulateSelectLists()
        {
            try
            {
                SelectListOfSubjects = new List<string>()
                {
                    "Select...",
                    "Contributing",
                    "Request Membership",
                    "Bug Report"
                };
            }
            catch (Exception ex)
            {
                GetInnerException(ex);
            }
        }

        private string GetInnerException(Exception ex)
        {
            Exception rootCause = ex;
            while (rootCause.InnerException != null)
                rootCause = rootCause.InnerException;
            return rootCause.Message;
        }
    }
}
