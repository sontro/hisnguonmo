using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTransfusionSum
{
    partial class HisTransfusionSumCheck : EntityBase
    {
        public HisTransfusionSumCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSFUSION_SUM>();
        }

        private BridgeDAO<HIS_TRANSFUSION_SUM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
