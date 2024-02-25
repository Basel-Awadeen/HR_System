using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IVacationService
    {
        public Task<string> UpadeVacationRequests(string approverEmail , int vacationId , VacationRequestStatus status );
        public Task<List<Vacation>> GetVacations(string email);

    }
}
