using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDepartment
{
    partial class HisDepartmentTruncate : EntityBase
    {
        public HisDepartmentTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEPARTMENT>();
        }

        private BridgeDAO<HIS_DEPARTMENT> bridgeDAO;

        public bool Truncate(HIS_DEPARTMENT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DEPARTMENT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
