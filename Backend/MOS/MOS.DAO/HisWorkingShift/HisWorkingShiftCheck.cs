using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisWorkingShift
{
    partial class HisWorkingShiftCheck : EntityBase
    {
        public HisWorkingShiftCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_WORKING_SHIFT>();
        }

        private BridgeDAO<HIS_WORKING_SHIFT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
