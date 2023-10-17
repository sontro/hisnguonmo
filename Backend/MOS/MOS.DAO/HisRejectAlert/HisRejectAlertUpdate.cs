using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRejectAlert
{
    partial class HisRejectAlertUpdate : EntityBase
    {
        public HisRejectAlertUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REJECT_ALERT>();
        }

        private BridgeDAO<HIS_REJECT_ALERT> bridgeDAO;

        public bool Update(HIS_REJECT_ALERT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_REJECT_ALERT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
