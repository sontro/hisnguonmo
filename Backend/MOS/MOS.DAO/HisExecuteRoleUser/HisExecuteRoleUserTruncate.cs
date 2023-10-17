using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExecuteRoleUser
{
    partial class HisExecuteRoleUserTruncate : EntityBase
    {
        public HisExecuteRoleUserTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXECUTE_ROLE_USER>();
        }

        private BridgeDAO<HIS_EXECUTE_ROLE_USER> bridgeDAO;

        public bool Truncate(HIS_EXECUTE_ROLE_USER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXECUTE_ROLE_USER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
