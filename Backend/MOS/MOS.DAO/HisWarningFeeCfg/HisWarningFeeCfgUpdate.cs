using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisWarningFeeCfg
{
    partial class HisWarningFeeCfgUpdate : EntityBase
    {
        public HisWarningFeeCfgUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_WARNING_FEE_CFG>();
        }

        private BridgeDAO<HIS_WARNING_FEE_CFG> bridgeDAO;

        public bool Update(HIS_WARNING_FEE_CFG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_WARNING_FEE_CFG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
