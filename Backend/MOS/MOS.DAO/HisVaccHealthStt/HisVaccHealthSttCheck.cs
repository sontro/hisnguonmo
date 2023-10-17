using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisVaccHealthStt
{
    partial class HisVaccHealthSttCheck : EntityBase
    {
        public HisVaccHealthSttCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_HEALTH_STT>();
        }

        private BridgeDAO<HIS_VACC_HEALTH_STT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
