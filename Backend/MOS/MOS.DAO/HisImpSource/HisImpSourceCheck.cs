using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisImpSource
{
    partial class HisImpSourceCheck : EntityBase
    {
        public HisImpSourceCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_SOURCE>();
        }

        private BridgeDAO<HIS_IMP_SOURCE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
