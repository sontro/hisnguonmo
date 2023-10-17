using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMediContractMatySO : StagingObjectBase
    {
        public HisMediContractMatySO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MEDI_CONTRACT_MATY, bool>>> listHisMediContractMatyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDI_CONTRACT_MATY, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_CONTRACT_MATY, bool>>> listVHisMediContractMatyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_CONTRACT_MATY, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_CONTRACT_MATY_1, bool>>> listV1HisMediContractMatyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_CONTRACT_MATY_1, bool>>>();
    }
}
