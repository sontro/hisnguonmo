using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisWorkingShift
{
    partial class HisWorkingShiftTruncate : EntityBase
    {
        public HisWorkingShiftTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_WORKING_SHIFT>();
        }

        private BridgeDAO<HIS_WORKING_SHIFT> bridgeDAO;

        public bool Truncate(HIS_WORKING_SHIFT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_WORKING_SHIFT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
