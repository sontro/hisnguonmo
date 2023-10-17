using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMediContractMetySO : StagingObjectBase
    {
        public HisMediContractMetySO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MEDI_CONTRACT_METY, bool>>> listHisMediContractMetyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDI_CONTRACT_METY, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_CONTRACT_METY, bool>>> listVHisMediContractMetyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_CONTRACT_METY, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_CONTRACT_METY_1, bool>>> listV1HisMediContractMetyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_CONTRACT_METY_1, bool>>>();
    }
}
