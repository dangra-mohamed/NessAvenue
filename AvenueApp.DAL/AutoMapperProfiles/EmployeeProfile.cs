using AutoMapper;
using AvenueApp.Models;
using Models.ViewModels;


namespace DAL.AutoMapperProfiles
{
    public class EmployeeProfile: Profile
    {
        public EmployeeProfile() {
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<EmployeeViewModel, Employee>();
        }
    }
}
