using AutoMapper;
using AvenueApp.DAL;
using AvenueApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace DAL.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context = null;        
        private readonly IMapper _mapper;

        public EmployeeRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper =   mapper;
        }

        public async Task<int> AddNewEmployee(EmployeeViewModel employee)
        {

            //await _context.Employees.AddAsync(_mapper.Map<Employee>(employee));
            //await _context.SaveChangesAsync();

            //return employee.Id;
            var emp = _mapper.Map<Employee>(employee);
            _context.Employees.Add(emp); 
            await _context.SaveChangesAsync();
            return emp.Id;
        }

        public async Task<List<EmployeeViewModel>> GetAllEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return _mapper.Map<List<EmployeeViewModel>>(employees.Select(emp=>emp)); 
      
        }
    }
}
