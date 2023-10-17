using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisUserGroupTemp
{
    partial class HisUserGroupTempUpdate : EntityBase
    {
        public HisUserGroupTempUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_USER_GROUP_TEMP>();
        }

        private BridgeDAO<HIS_USER_GROUP_TEMP> bridgeDAO;

        public bool Update(HIS_USER_GROUP_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_USER_GROUP_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
