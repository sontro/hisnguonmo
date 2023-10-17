using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisInfusion
{
    partial class HisInfusionCheck : EntityBase
    {
        public HisInfusionCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INFUSION>();
        }

        private BridgeDAO<HIS_INFUSION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
