using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisDocumentBookSO : StagingObjectBase
    {
        public HisDocumentBookSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_DOCUMENT_BOOK, bool>>> listHisDocumentBookExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DOCUMENT_BOOK, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_DOCUMENT_BOOK, bool>>> listVHisDocumentBookExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_DOCUMENT_BOOK, bool>>>();
    }
}
