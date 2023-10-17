using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisExeServiceModuleSO : StagingObjectBase
    {
        public HisExeServiceModuleSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_EXE_SERVICE_MODULE, bool>>> listHisExeServiceModuleExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EXE_SERVICE_MODULE, bool>>>();
    }
}
