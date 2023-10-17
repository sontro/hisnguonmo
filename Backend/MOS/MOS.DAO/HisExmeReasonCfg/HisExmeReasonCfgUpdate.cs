using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExmeReasonCfg
{
    partial class HisExmeReasonCfgUpdate : EntityBase
    {
        public HisExmeReasonCfgUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXME_REASON_CFG>();
        }

        private BridgeDAO<HIS_EXME_REASON_CFG> bridgeDAO;

        public bool Update(HIS_EXME_REASON_CFG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXME_REASON_CFG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
