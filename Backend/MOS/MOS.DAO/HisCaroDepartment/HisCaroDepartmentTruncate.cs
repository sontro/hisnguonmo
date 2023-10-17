using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCaroDepartment
{
    partial class HisCaroDepartmentTruncate : EntityBase
    {
        public HisCaroDepartmentTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARO_DEPARTMENT>();
        }

        private BridgeDAO<HIS_CARO_DEPARTMENT> bridgeDAO;

        public bool Truncate(HIS_CARO_DEPARTMENT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CARO_DEPARTMENT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
