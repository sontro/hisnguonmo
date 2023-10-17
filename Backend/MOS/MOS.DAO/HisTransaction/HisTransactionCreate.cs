using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTransaction
{
    partial class HisTransactionCreate : EntityBase
    {
        public HisTransactionCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSACTION>();
        }

        private BridgeDAO<HIS_TRANSACTION> bridgeDAO;

        public bool Create(HIS_TRANSACTION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TRANSACTION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
