using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisUserGroupTempDt
{
    partial class HisUserGroupTempDtUpdate : EntityBase
    {
        public HisUserGroupTempDtUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_USER_GROUP_TEMP_DT>();
        }

        private BridgeDAO<HIS_USER_GROUP_TEMP_DT> bridgeDAO;

        public bool Update(HIS_USER_GROUP_TEMP_DT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_USER_GROUP_TEMP_DT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
