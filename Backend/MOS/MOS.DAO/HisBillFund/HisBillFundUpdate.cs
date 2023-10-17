using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBillFund
{
    partial class HisBillFundUpdate : EntityBase
    {
        public HisBillFundUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BILL_FUND>();
        }

        private BridgeDAO<HIS_BILL_FUND> bridgeDAO;

        public bool Update(HIS_BILL_FUND data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BILL_FUND> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
