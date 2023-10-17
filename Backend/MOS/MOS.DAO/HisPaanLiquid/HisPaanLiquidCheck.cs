using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPaanLiquid
{
    partial class HisPaanLiquidCheck : EntityBase
    {
        public HisPaanLiquidCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PAAN_LIQUID>();
        }

        private BridgeDAO<HIS_PAAN_LIQUID> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
