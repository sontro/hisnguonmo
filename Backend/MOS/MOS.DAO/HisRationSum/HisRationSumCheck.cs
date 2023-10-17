using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRationSum
{
    partial class HisRationSumCheck : EntityBase
    {
        public HisRationSumCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_SUM>();
        }

        private BridgeDAO<HIS_RATION_SUM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
