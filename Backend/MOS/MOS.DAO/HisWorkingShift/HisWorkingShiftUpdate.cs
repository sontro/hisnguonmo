using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisWorkingShift
{
    partial class HisWorkingShiftUpdate : EntityBase
    {
        public HisWorkingShiftUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_WORKING_SHIFT>();
        }

        private BridgeDAO<HIS_WORKING_SHIFT> bridgeDAO;

        public bool Update(HIS_WORKING_SHIFT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_WORKING_SHIFT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
