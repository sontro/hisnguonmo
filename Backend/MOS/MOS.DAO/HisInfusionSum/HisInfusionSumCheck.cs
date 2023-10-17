using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisInfusionSum
{
    partial class HisInfusionSumCheck : EntityBase
    {
        public HisInfusionSumCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INFUSION_SUM>();
        }

        private BridgeDAO<HIS_INFUSION_SUM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
