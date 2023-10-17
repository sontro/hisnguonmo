using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmployee
{
    partial class HisEmployeeTruncate : EntityBase
    {
        public HisEmployeeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMPLOYEE>();
        }

        private BridgeDAO<HIS_EMPLOYEE> bridgeDAO;

        public bool Truncate(HIS_EMPLOYEE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EMPLOYEE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
