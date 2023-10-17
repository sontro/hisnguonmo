using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPtttGroupBest
{
    partial class HisPtttGroupBestCheck : EntityBase
    {
        public HisPtttGroupBestCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_GROUP_BEST>();
        }

        private BridgeDAO<HIS_PTTT_GROUP_BEST> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
