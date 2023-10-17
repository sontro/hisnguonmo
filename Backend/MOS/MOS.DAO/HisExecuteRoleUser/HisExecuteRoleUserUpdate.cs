using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExecuteRoleUser
{
    partial class HisExecuteRoleUserUpdate : EntityBase
    {
        public HisExecuteRoleUserUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXECUTE_ROLE_USER>();
        }

        private BridgeDAO<HIS_EXECUTE_ROLE_USER> bridgeDAO;

        public bool Update(HIS_EXECUTE_ROLE_USER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXECUTE_ROLE_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
