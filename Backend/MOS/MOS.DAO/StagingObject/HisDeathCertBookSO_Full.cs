using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisDeathCertBookSO : StagingObjectBase
    {
        public HisDeathCertBookSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_DEATH_CERT_BOOK, bool>>> listHisDeathCertBookExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DEATH_CERT_BOOK, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_DEATH_CERT_BOOK, bool>>> listVHisDeathCertBookExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_DEATH_CERT_BOOK, bool>>>();
    }
}
