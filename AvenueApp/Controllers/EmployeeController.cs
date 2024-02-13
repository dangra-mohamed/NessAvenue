using AvenueApp.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using System.Diagnostics;

namespace AvenueApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository = null;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }              

        public async Task<IActionResult> Index()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();  

            ViewBag.Employeeslst = await _employeeRepository.GetAllEmployees();
            return View(employeeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(EmployeeViewModel employee)
        {

            if (ModelState.IsValid)
            {
                // Save the employee to the database or list
                int id = await _employeeRepository.AddNewEmployee(employee);

                ModelState.Clear();

                if (id > 0)
                    return RedirectToAction("Index");

            }
            return View();
        }


    }
}
