using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTransactionExp
{
    partial class HisTransactionExpCreate : EntityBase
    {
        public HisTransactionExpCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSACTION_EXP>();
        }

        private BridgeDAO<HIS_TRANSACTION_EXP> bridgeDAO;

        public bool Create(HIS_TRANSACTION_EXP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TRANSACTION_EXP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
