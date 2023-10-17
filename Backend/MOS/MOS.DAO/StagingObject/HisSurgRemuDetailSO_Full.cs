using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisSurgRemuDetailSO : StagingObjectBase
    {
        public HisSurgRemuDetailSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SURG_REMU_DETAIL, bool>>> listHisSurgRemuDetailExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SURG_REMU_DETAIL, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SURG_REMU_DETAIL, bool>>> listVHisSurgRemuDetailExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SURG_REMU_DETAIL, bool>>>();
    }
}
