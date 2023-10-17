using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDepositReq
{
    partial class HisDepositReqCreate : EntityBase
    {
        public HisDepositReqCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEPOSIT_REQ>();
        }

        private BridgeDAO<HIS_DEPOSIT_REQ> bridgeDAO;

        public bool Create(HIS_DEPOSIT_REQ data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DEPOSIT_REQ> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
