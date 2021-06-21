using DotnetCoreRazorWebApp.Models;
using DotnetCoreRazorWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetCoreRazorWebApp.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private IEmployeeRepository _employeeRepository { get; set; }
        public Employee Employee { get; private set; }

        //[BindProperty(SupportsGet =true)]
        //public string Message { get; set; }

        [TempData]
        public string Message { get; set; }
        public DetailsModel(IEmployeeRepository employeeRepository)
        {
           this._employeeRepository = employeeRepository;
        }
        public IActionResult OnGet(int id)
        {
           Employee = this._employeeRepository.GetEmployee(id);

            if (Employee == null)
            {
                return RedirectToPage("/Notfound");
            }

            return Page();
        }
    }
}
