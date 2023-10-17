using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCancelReason
{
    partial class HisCancelReasonUpdate : EntityBase
    {
        public HisCancelReasonUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CANCEL_REASON>();
        }

        private BridgeDAO<HIS_CANCEL_REASON> bridgeDAO;

        public bool Update(HIS_CANCEL_REASON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CANCEL_REASON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
