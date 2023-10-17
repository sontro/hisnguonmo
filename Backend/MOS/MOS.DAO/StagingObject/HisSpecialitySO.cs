using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisSpecialitySO : StagingObjectBase
    {
        public HisSpecialitySO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SPECIALITY, bool>>> listHisSpecialityExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SPECIALITY, bool>>>();
    }
}
