using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisImpMestPay
{
    partial class HisImpMestPayCheck : EntityBase
    {
        public HisImpMestPayCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_PAY>();
        }

        private BridgeDAO<HIS_IMP_MEST_PAY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
