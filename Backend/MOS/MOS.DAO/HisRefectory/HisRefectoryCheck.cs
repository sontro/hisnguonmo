using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRefectory
{
    partial class HisRefectoryCheck : EntityBase
    {
        public HisRefectoryCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REFECTORY>();
        }

        private BridgeDAO<HIS_REFECTORY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
