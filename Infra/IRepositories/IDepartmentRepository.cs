using Domain.Entities;
using Infra.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.IRepositories
{
    public interface IDepartmentRepository: IGenericRepositoryAsync<Department>
    {
        public Task<List<Department>> GetDepartmentsByCompanyIdAsync(int companyId);

    }
}
