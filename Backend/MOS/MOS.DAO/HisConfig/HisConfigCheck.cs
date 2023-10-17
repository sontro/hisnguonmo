using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisConfig
{
    partial class HisConfigCheck : EntityBase
    {
        public HisConfigCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONFIG>();
        }

        private BridgeDAO<HIS_CONFIG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
