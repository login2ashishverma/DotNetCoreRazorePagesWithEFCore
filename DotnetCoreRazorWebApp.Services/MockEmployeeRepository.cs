using DotnetCoreRazorWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace DotnetCoreRazorWebApp.Services
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Mary", Department = Dept.HR,
                    Email = "mary@pragimtech.com", PhotoPath="merry.jpg" },
                new Employee() { Id = 2, Name = "John", Department = Dept.IT,
                    Email = "john@pragimtech.com", PhotoPath="john.jpg" },
                new Employee() { Id = 3, Name = "Sara", Department = Dept.IT,
                    Email = "sara@pragimtech.com", PhotoPath="saara.jpg" },
                new Employee() { Id = 4, Name = "David", Department = Dept.Payroll,
                    Email = "david@pragimtech.com" },
            };
        }

        public Employee AddEmployee(Employee newEmployee)
        {
            newEmployee.Id = _employeeList.Max(e => e.Id) + 1;

            _employeeList.Add(newEmployee);

            return newEmployee;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee deletedEmployee = _employeeList.FirstOrDefault(e => e.Id == id);

            if (deletedEmployee != null)
            {
               _employeeList.Remove(deletedEmployee);
            }

            return deletedEmployee;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDepartment(Dept? dept)
        {
            IEnumerable<Employee> query = _employeeList;

            if (dept.HasValue)
            {
                query = query.Where(q => q.Department == dept.Value);
            }
            return query.GroupBy(e => e.Department)
                                .Select(g => new DeptHeadCount()
                                {
                                    Department = g.Key.Value,
                                    Count = g.Count()
                                }).ToList();
        }

        public IEnumerable<Employee> Search(string searchTerm = null)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return _employeeList;
            }

            return _employeeList.Where(e => e.Name.Contains(searchTerm) ||
                                            e.Email.Contains(searchTerm)).ToList();
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int? id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id.Value);
        }

        public Employee UpdateEmployee(Employee updatedEmployee)
        {
            var employee = _employeeList.FirstOrDefault(e => e.Id == updatedEmployee?.Id);

            if (employee != null)
            {
                employee.Name = updatedEmployee?.Name;
                employee.Department = updatedEmployee?.Department;
                employee.Email = updatedEmployee?.Email;
                employee.PhotoPath = updatedEmployee?.PhotoPath;
            }
            return employee;
        }
    }
}
