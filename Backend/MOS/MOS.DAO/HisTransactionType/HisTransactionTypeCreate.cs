using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTransactionType
{
    partial class HisTransactionTypeCreate : EntityBase
    {
        public HisTransactionTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSACTION_TYPE>();
        }

        private BridgeDAO<HIS_TRANSACTION_TYPE> bridgeDAO;

        public bool Create(HIS_TRANSACTION_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TRANSACTION_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
