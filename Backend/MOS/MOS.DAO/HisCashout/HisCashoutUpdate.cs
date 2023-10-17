using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCashout
{
    partial class HisCashoutUpdate : EntityBase
    {
        public HisCashoutUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CASHOUT>();
        }

        private BridgeDAO<HIS_CASHOUT> bridgeDAO;

        public bool Update(HIS_CASHOUT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CASHOUT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
