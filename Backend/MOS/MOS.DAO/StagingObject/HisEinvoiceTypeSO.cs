using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisEinvoiceTypeSO : StagingObjectBase
    {
        public HisEinvoiceTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_EINVOICE_TYPE, bool>>> listHisEinvoiceTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EINVOICE_TYPE, bool>>>();
    }
}
