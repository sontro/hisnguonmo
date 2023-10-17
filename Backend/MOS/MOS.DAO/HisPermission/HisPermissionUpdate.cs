using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPermission
{
    partial class HisPermissionUpdate : EntityBase
    {
        public HisPermissionUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PERMISSION>();
        }

        private BridgeDAO<HIS_PERMISSION> bridgeDAO;

        public bool Update(HIS_PERMISSION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PERMISSION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
