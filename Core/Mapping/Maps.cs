using AutoMapper;
using Core.Featuers.Account.Command.Models;
using Core.Featuers.Attendace.Queries.Results;
using Core.Featuers.Company.Command.Queries;
using Core.Featuers.Company.Queries.Results;
using Core.Featuers.Department.Command.Models;
using Core.Featuers.Vacation.Queries.Result;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Featuers.Department.Command.Models.AddNewDepartments;

namespace Core.Mapping
{
    public partial class MappingProfile 
    {
        public void AddMaps()
        {
            CreateMap<CreateCompanyCommand, Company>();
            CreateMap<AddNewDepartments, List<Department>>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<RegisterDTO, Employee>();
            CreateMap<Employee, All_Employee_Response>();
            CreateMap<Vacation, AllVacations>();
            CreateMap<Attendance, AllEmployeeAtendanceResponse>();


        }
    }
}
