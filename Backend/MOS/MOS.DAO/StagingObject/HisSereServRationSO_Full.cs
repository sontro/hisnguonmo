using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisSereServRationSO : StagingObjectBase
    {
        public HisSereServRationSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SERE_SERV_RATION, bool>>> listHisSereServRationExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERE_SERV_RATION, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_RATION, bool>>> listVHisSereServRationExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_RATION, bool>>>();
    }
}
