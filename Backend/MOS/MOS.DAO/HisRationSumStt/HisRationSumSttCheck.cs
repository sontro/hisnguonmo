using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRationSumStt
{
    partial class HisRationSumSttCheck : EntityBase
    {
        public HisRationSumSttCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_SUM_STT>();
        }

        private BridgeDAO<HIS_RATION_SUM_STT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
