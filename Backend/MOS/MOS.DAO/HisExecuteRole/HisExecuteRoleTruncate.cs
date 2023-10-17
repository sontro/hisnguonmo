using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExecuteRole
{
    partial class HisExecuteRoleTruncate : EntityBase
    {
        public HisExecuteRoleTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXECUTE_ROLE>();
        }

        private BridgeDAO<HIS_EXECUTE_ROLE> bridgeDAO;

        public bool Truncate(HIS_EXECUTE_ROLE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXECUTE_ROLE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
