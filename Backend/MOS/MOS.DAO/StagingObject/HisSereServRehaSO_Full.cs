using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisSereServRehaSO : StagingObjectBase
    {
        public HisSereServRehaSO()
        {
            listHisSereServRehaExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
            listVHisSereServRehaExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SERE_SERV_REHA, bool>>> listHisSereServRehaExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERE_SERV_REHA, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_REHA, bool>>> listVHisSereServRehaExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_REHA, bool>>>();
    }
}
