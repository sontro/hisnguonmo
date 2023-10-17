using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExecuteRole
{
    partial class HisExecuteRoleUpdate : EntityBase
    {
        public HisExecuteRoleUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXECUTE_ROLE>();
        }

        private BridgeDAO<HIS_EXECUTE_ROLE> bridgeDAO;

        public bool Update(HIS_EXECUTE_ROLE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXECUTE_ROLE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
