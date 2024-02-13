using System;
using Xunit;
using Moq;

using System.Collections.Generic;
using System.Linq;

public class EmployeeControllerTests
{
    [Fact]
    public void Index_ReturnsAViewResult_WithAListOfEmployees()
    {
        // Arrange
        var mockRepo = new Mock<IEmployeeRepository>();
        mockRepo.Setup(repo => repo.GetAllEmployees())
            .Returns(GetTestEmployees());
        var controller = new EmployeeController(mockRepo.Object);

        // Act
        var result = controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<Employee>>(
            viewResult.ViewData.Model);
        Assert.Equal(3, model.Count);
    }

    [Fact]
    public void Create_ReturnsViewResult_WithEmployeeModel()
    {
        // Arrange
        var mockRepo = new Mock<IEmployeeRepository>();
        var controller = new EmployeeController(mockRepo.Object);

        // Act
        var result = controller.Create();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Employee>(viewResult.ViewData.Model);
        Assert.Null(model.Name);
    }

    [Fact]
    public void Create_InvalidModelState_ReturnsViewResult()
    {
        // Arrange
        var mockRepo = new Mock<IEmployeeRepository>();
        var controller = new EmployeeController(mockRepo.Object);
        controller.ModelState.AddModelError("Name", "Required");
        var employee = new Employee();

        // Act
        var result = controller.Create(employee);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(employee, viewResult.ViewData.Model);
    }

    [Fact]
    public void Create_ValidModelState_RedirectsToIndexAction()
    {
        // Arrange
        var mockRepo = new Mock<IEmployeeRepository>();
        var controller = new EmployeeController(mockRepo.Object);
        var employee = new Employee { Name = "John", DateOfBirth = DateTime.Now, Salary = 50000 };

        // Act
        var result = controller.Create(employee);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
    }

    private List<Employee> GetTestEmployees()
    {
        var employees = new List<Employee>();
        employees.Add(new Employee { Id = 1, Name = "John", DateOfBirth = new DateTime(1990, 1, 1), Salary = 50000 });
        employees.Add(new Employee { Id = 2, Name = "Jane", DateOfBirth = new DateTime(1995, 5, 10), Salary = 60000 });
        employees.Add(new Employee { Id = 3, Name = "Doe", DateOfBirth = new DateTime(1985, 8, 3), Salary = 55000 });
        return employees;
    }
}
