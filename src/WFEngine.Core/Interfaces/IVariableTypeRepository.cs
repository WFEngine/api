using System.Collections.Generic;
using WFEngine.Core.Entities;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Core.Interfaces
{
    public interface IVariableTypeRepository
    {
        IDataResult<List<VariableType>> GetVariableTypes(int packageVersionId);
    }
}
