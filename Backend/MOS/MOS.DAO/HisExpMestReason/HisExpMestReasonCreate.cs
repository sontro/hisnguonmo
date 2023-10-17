using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestReason
{
    partial class HisExpMestReasonCreate : EntityBase
    {
        public HisExpMestReasonCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_REASON>();
        }

        private BridgeDAO<HIS_EXP_MEST_REASON> bridgeDAO;

        public bool Create(HIS_EXP_MEST_REASON data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXP_MEST_REASON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
