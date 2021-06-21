using DotnetCoreRazorWebApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotnetCoreRazorWebApp.Services
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public SqlEmployeeRepository(AppDbContext context)
        {
            this._context = context;
        }

        public Employee AddEmployee(Employee newEmployee)
        {
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
            return newEmployee;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);

            if (employee != null)
            {
               _context.Employees.Remove(employee);
               _context.SaveChanges();
            }

            return employee;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDepartment(Dept? dept)
        {
            IEnumerable<Employee> query = _context.Employees;

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

        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return _context.Employees;
            }

            return _context.Employees.Where(e => e.Name.Contains(searchTerm) ||
                                            e.Email.Contains(searchTerm)).ToList();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            //return _context.Employees;
            return _context.Employees
                .FromSqlRaw<Employee>("select * from employee").ToList();
        }

        public Employee GetEmployee(int? id)
        {
            //return _context.Employees.FirstOrDefault(e => e.Id == id.Value);
            //return _context.Employees.FromSqlRaw<Employee>("spGetEmployeeById {0}", id).ToList().FirstOrDefault();

            SqlParameter param = new SqlParameter("@Id", id);

            return _context.Employees.FromSqlRaw<Employee>("spGetEmployeeById @Id", param).ToList().FirstOrDefault();
        }

        public Employee UpdateEmployee(Employee updatedEmployee)
        {
            var employee = _context.Employees.Attach(updatedEmployee);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return updatedEmployee;
        }
    }
}
