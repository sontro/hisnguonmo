using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisFormTypeCfg
{
    partial class HisFormTypeCfgUpdate : EntityBase
    {
        public HisFormTypeCfgUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FORM_TYPE_CFG>();
        }

        private BridgeDAO<HIS_FORM_TYPE_CFG> bridgeDAO;

        public bool Update(HIS_FORM_TYPE_CFG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_FORM_TYPE_CFG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
