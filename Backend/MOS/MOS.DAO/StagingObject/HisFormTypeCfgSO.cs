using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisFormTypeCfgSO : StagingObjectBase
    {
        public HisFormTypeCfgSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_FORM_TYPE_CFG, bool>>> listHisFormTypeCfgExpression = new List<System.Linq.Expressions.Expression<Func<HIS_FORM_TYPE_CFG, bool>>>();
    }
}
