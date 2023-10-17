using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisHospitalizeReason
{
    partial class HisHospitalizeReasonCheck : EntityBase
    {
        public HisHospitalizeReasonCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HOSPITALIZE_REASON>();
        }

        private BridgeDAO<HIS_HOSPITALIZE_REASON> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
