using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCoreRazorWebApp.Models;
using DotnetCoreRazorWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetCoreRazorWebApp.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private IEmployeeRepository _employeeRepository { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public String SearchTerm { get; set; }
        public IndexModel(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }
        public string Message { get; set; }
        public void OnGet(string SearchTerm)
        {
            Employees = this._employeeRepository.Search(SearchTerm);
        }
    }
}
