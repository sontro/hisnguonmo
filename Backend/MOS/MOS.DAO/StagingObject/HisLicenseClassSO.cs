using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisLicenseClassSO : StagingObjectBase
    {
        public HisLicenseClassSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_LICENSE_CLASS, bool>>> listHisLicenseClassExpression = new List<System.Linq.Expressions.Expression<Func<HIS_LICENSE_CLASS, bool>>>();
    }
}
