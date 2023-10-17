using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpiredDateCfg
{
    partial class HisExpiredDateCfgUpdate : EntityBase
    {
        public HisExpiredDateCfgUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXPIRED_DATE_CFG>();
        }

        private BridgeDAO<HIS_EXPIRED_DATE_CFG> bridgeDAO;

        public bool Update(HIS_EXPIRED_DATE_CFG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXPIRED_DATE_CFG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
