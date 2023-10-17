using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAlert
{
    partial class HisAlertUpdate : EntityBase
    {
        public HisAlertUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ALERT>();
        }

        private BridgeDAO<HIS_ALERT> bridgeDAO;

        public bool Update(HIS_ALERT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ALERT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
