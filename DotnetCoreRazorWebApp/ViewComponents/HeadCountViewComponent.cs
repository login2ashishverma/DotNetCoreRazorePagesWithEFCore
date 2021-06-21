using DotnetCoreRazorWebApp.Models;
using DotnetCoreRazorWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreRazorWebApp.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly IEmployeeRepository employeeRepository;

        public IEnumerable<DeptHeadCount> DeptHeadCount { get; set; }
        public HeadCountViewComponent(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public IViewComponentResult Invoke(Dept? dept= null)
        {
            DeptHeadCount = this.employeeRepository.EmployeeCountByDepartment(dept);

            return View(DeptHeadCount);
        }
    }
}
