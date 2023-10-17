using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPtttHighTech
{
    partial class HisPtttHighTechCheck : EntityBase
    {
        public HisPtttHighTechCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_HIGH_TECH>();
        }

        private BridgeDAO<HIS_PTTT_HIGH_TECH> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
