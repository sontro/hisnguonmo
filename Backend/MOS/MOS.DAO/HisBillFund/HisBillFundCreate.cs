using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBillFund
{
    partial class HisBillFundCreate : EntityBase
    {
        public HisBillFundCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BILL_FUND>();
        }

        private BridgeDAO<HIS_BILL_FUND> bridgeDAO;

        public bool Create(HIS_BILL_FUND data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BILL_FUND> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
