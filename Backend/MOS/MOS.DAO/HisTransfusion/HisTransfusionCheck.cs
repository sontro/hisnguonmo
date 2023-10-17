using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTransfusion
{
    partial class HisTransfusionCheck : EntityBase
    {
        public HisTransfusionCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSFUSION>();
        }

        private BridgeDAO<HIS_TRANSFUSION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
