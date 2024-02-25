using Domain.Entities;
using Infra.Bases;
using Infra.DBContext;
using Infra.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class CompanyRepository: GenericRepositoryAsync<Company>, ICompanyRepository
    {
        #region Fields
        private readonly DbSet<Company> companies;
        #endregion

        #region Constructors
        public CompanyRepository(AppDbContext dbContext) : base(dbContext)
        {
            companies = dbContext.Set<Company>();
        }
        #endregion
    }
}
