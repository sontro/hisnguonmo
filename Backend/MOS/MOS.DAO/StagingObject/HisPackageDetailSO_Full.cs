using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisPackageDetailSO : StagingObjectBase
    {
        public HisPackageDetailSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_PACKAGE_DETAIL, bool>>> listHisPackageDetailExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PACKAGE_DETAIL, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_PACKAGE_DETAIL, bool>>> listVHisPackageDetailExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PACKAGE_DETAIL, bool>>>();
    }
}
