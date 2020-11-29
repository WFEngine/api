using System.Data;
using System.Linq;
using WFEngine.Core.Entities;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Service.Repositories
{
    public class OrganizationRepository : BaseRepository, IOrganizationRepository
    {
        public OrganizationRepository(IDbTransaction transaction) 
            : base(transaction)
        {
        }

        public IDataResult<Organization> FindByName(string name)
        {
            var organization = connection.ExecuteCommand<Organization>("SELECT * FROM organization WHERE Name = @name AND Status = 1",name)?.FirstOrDefault();
            if (organization != null)
                return new SuccessDataResult<Organization>(organization);
            return new ErrorDataResult<Organization>(null, Messages.Organization.NotFoundOrganization);
        }

        public IResult Insert(Organization organization)
        {
            organization.Id = connection.Insert(organization);
            if (organization.Id >0)
                return new SuccessResult();
            return new ErrorResult(Messages.Organization.NotCreatedOrganization);            
        }

        public IDataResult<Organization> FindById(int id)
        {
            var organization = connection.ExecuteCommand<Organization>("SELECT * FROM organization WHERE Id = @id AND Status = 1", id)?.FirstOrDefault();
            if (organization != null)
                return new SuccessDataResult<Organization>(organization);
            return new ErrorDataResult<Organization>(null, Messages.Organization.NotFoundOrganization);
        }
    }
}
