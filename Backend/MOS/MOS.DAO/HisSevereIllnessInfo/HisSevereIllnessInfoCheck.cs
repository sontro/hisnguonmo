using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSevereIllnessInfo
{
    partial class HisSevereIllnessInfoCheck : EntityBase
    {
        public HisSevereIllnessInfoCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SEVERE_ILLNESS_INFO>();
        }

        private BridgeDAO<HIS_SEVERE_ILLNESS_INFO> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
