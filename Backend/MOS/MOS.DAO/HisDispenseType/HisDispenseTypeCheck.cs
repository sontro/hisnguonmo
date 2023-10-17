using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDispenseType
{
    partial class HisDispenseTypeCheck : EntityBase
    {
        public HisDispenseTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DISPENSE_TYPE>();
        }

        private BridgeDAO<HIS_DISPENSE_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
