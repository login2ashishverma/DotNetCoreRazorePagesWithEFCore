using DotnetCoreRazorWebApp.Models;
using System;
using System.Collections.Generic;

namespace DotnetCoreRazorWebApp.Services
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Search(string searchTerm);
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(int? id);
        Employee UpdateEmployee(Employee updatedEmployee);
        Employee AddEmployee(Employee newEmployee);
        Employee DeleteEmployee(int id);
        IEnumerable<DeptHeadCount> EmployeeCountByDepartment(Dept? dept);
    }
}
