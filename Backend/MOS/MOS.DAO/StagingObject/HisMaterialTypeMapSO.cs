using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMaterialTypeMapSO : StagingObjectBase
    {
        public HisMaterialTypeMapSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MATERIAL_TYPE_MAP, bool>>> listHisMaterialTypeMapExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MATERIAL_TYPE_MAP, bool>>>();
    }
}
