using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisConfigGroup
{
    partial class HisConfigGroupUpdate : EntityBase
    {
        public HisConfigGroupUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONFIG_GROUP>();
        }

        private BridgeDAO<HIS_CONFIG_GROUP> bridgeDAO;

        public bool Update(HIS_CONFIG_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CONFIG_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
