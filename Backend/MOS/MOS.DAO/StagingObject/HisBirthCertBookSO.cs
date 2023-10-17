using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisBirthCertBookSO : StagingObjectBase
    {
        public HisBirthCertBookSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_BIRTH_CERT_BOOK, bool>>> listHisBirthCertBookExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BIRTH_CERT_BOOK, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_BIRTH_CERT_BOOK, bool>>> listVHisBirthCertBookExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BIRTH_CERT_BOOK, bool>>>();
    }
}
