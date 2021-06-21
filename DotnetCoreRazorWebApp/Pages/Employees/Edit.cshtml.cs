using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotnetCoreRazorWebApp.Models;
using DotnetCoreRazorWebApp.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetCoreRazorWebApp.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        [BindProperty]
        public Employee Employee { get; set; }

        // We use this property to store and process
        // the newly uploaded photo
        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public bool Notify { get; set; }
        public string Message { get; set; }

        public EditModel(IEmployeeRepository employeeRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            this.employeeRepository = employeeRepository;
            this.webHostEnvironment = webHostEnvironment; // IWebHostEnvironment service allows us to get the
            // absolute path of WWWRoot folder
        }

        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
               Employee = employeeRepository.GetEmployee(id.Value);
            }
            else
            {
                Employee = new Employee();
            }

            if (Employee == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    // If a new photo is uploaded, the existing photo must be
                    // deleted. So check if there is an existing photo and delete
                    if (Employee.PhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                            "images", Employee.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    // Save the new photo in wwwroot/images folder and update
                    // PhotoPath property of the employee object
                    Employee.PhotoPath = ProcessUploadedFile();
                }

                if (Employee != null 
                    && Employee.Id > 0)
                {
                    Employee = employeeRepository.UpdateEmployee(Employee);
                }
                else
                {
                    Employee = employeeRepository.AddEmployee(Employee);
                }
                return RedirectToPage("Index");
            }
            return Page();
        }

        public IActionResult OnPostUpdateNotificationPreferences(int id)
        {
            if (Notify)
            {
                Message = "Thank you for turning on notifications";
            }
            else
            {
                Message = "You have turned off email notifications";
            }

            TempData["message"] = Message;
            return RedirectToPage("Details", new {id= id});
        }
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
