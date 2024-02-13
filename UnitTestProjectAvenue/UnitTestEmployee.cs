using System;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DAL.Repository;
using AvenueApp.Models;
using Models.ViewModels;
using AvenueApp.Controllers;
using Microsoft.EntityFrameworkCore;
using AvenueApp.DAL;
using Microsoft.AspNetCore.Hosting.Server;
using System.Text.RegularExpressions;

namespace UnitTestProjectAvenue
{
    public class EmployeeControllerTests 
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly Mock<IEmployeeRepository> dataRepository;

        public EmployeeControllerTests()
        {
            dataRepository = new Mock<IEmployeeRepository>();
        }

        private Task<List<EmployeeViewModel>> SeedTestData()
        {
            // Add test data to your DbContext entities
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
            employees.Add(new EmployeeViewModel {  Name = "Action1", DateOfBirth = new DateTime(1989, 10, 28), Salary = 50000 });
            employees.Add(new EmployeeViewModel {  Name = "SciFi1", DateOfBirth = new DateTime(1992, 5, 15), Salary = 60000 });
            employees.Add(new EmployeeViewModel {  Name = "History1", DateOfBirth = new DateTime(1985, 8, 3), Salary = 55000 });
            return Task.FromResult(employees);            
        }


        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfEmployees()
        {
            var groups = SeedTestData();
            dataRepository.Setup(x => x.GetAllEmployees()).Returns(groups);

            // Arrange            
            
            var controller = new EmployeeController(dataRepository.Object);

            // Act
            var result = await controller.Index() as ViewResult;

            var viewResult = result.ViewData["Employeeslst"] as List<EmployeeViewModel>;

            // Assert
            //var viewResult = Assert.IsType<ViewResult>(result);
            //var model = Assert.IsAssignableFrom<List<Employee>>(
                //viewResult.ViewData.Model);
            Assert.Equal(3, viewResult.Count);
        }

        //[Fact]
        //public void Create_ReturnsViewResult_WithEmployeeModel()
        //{
        //    // Arrange
        //    var mockRepo = new Mock<IEmployeeRepository>();
        //    var controller = new EmployeeController(mockRepo.Object);

        //    // Act
        //    var result = controller.Index();

        //    // Assert
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsType<Employee>(viewResult.ViewData.Model);
        //    Assert.Null(model.Name);
        //}

        //[Fact]
        //public void Create_InvalidModelState_ReturnsViewResult()
        //{
        //    // Arrange
        //    var mockRepo = new Mock<IEmployeeRepository>();
        //    var controller = new EmployeeController(mockRepo.Object);
        //    controller.ModelState.AddModelError("Name", "Required");
        //    var employee = new EmployeeViewModel();

        //    // Act
        //    var result = controller.Index(employee);

        //    // Assert
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    Assert.Equal(employee, viewResult.ViewData.Model);
        //}

        [Fact]
        public async Task Create_ValidModelState_RedirectsToIndexActionAsync()
        {
            var employee = new EmployeeViewModel { Name = "John123", DateOfBirth = DateTime.Now, Salary = 50000 };

            var groups = SeedTestData();
            dataRepository.Setup(x => x.GetAllEmployees()).Returns(groups);
            dataRepository.Setup(x => x.AddNewEmployee(employee)).Returns(Task.FromResult(1));

            // Arrange            
            var controller = new EmployeeController(dataRepository.Object);
            

            // Act
            var result = await controller.Index(employee);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Create_ValidModelState_RedirectsToIndexView()
        {
            var employee = new EmployeeViewModel { Name = "John123", DateOfBirth = DateTime.Now, Salary = 50000 };

            var groups = SeedTestData();
            dataRepository.Setup(x => x.GetAllEmployees()).Returns(groups);
            dataRepository.Setup(x => x.AddNewEmployee(employee)).Returns(Task.FromResult(0));

            // Arrange            
            var controller = new EmployeeController(dataRepository.Object);


            // Act
            var result = await controller.Index(employee) ;

            // Assert
            Assert.IsType<ViewResult>(result);
            //Assert.Equal("Index", redirectToActionResult.ActionName);
        }

    }
}
