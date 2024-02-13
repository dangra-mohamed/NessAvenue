using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IEmployeeRepository
    {
        public Task<List<EmployeeViewModel>> GetAllEmployees();
        public Task<int> AddNewEmployee(EmployeeViewModel employee);
    }
}
