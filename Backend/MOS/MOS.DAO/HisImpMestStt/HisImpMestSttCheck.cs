using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisImpMestStt
{
    partial class HisImpMestSttCheck : EntityBase
    {
        public HisImpMestSttCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_STT>();
        }

        private BridgeDAO<HIS_IMP_MEST_STT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
