using System.Collections.Generic;
using System.Data;
using System.Linq;
using WFEngine.Core.Entities;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Service.Repositories
{
    public class VariableTypeRepository : BaseRepository, IVariableTypeRepository
    {
        public VariableTypeRepository(IDbTransaction transaction) 
            : base(transaction)
        {
        }

        public IDataResult<List<VariableType>> GetVariableTypes(int packageVersionId)
        {
            var variableTypes = connection.ExecuteCommand<VariableType>(@"
            SELECT 
                v.*,
                (SELECT p.Version FROM packageversion p WHERE p.Id = v.PackageVersionId) AS 'PackageVersionName'
            FROM variabletype v WHERE v.PackageVersionId = @packageVersionId AND v.Status = 1;
            ", packageVersionId)?.ToList();
            return new SuccessDataResult<List<VariableType>>(variableTypes);
        }
    }
}
