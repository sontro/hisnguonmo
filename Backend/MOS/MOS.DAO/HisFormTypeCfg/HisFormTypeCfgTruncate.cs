using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisFormTypeCfg
{
    partial class HisFormTypeCfgTruncate : EntityBase
    {
        public HisFormTypeCfgTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FORM_TYPE_CFG>();
        }

        private BridgeDAO<HIS_FORM_TYPE_CFG> bridgeDAO;

        public bool Truncate(HIS_FORM_TYPE_CFG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_FORM_TYPE_CFG> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
