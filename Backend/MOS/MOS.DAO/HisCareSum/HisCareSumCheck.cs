using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisCareSum
{
    partial class HisCareSumCheck : EntityBase
    {
        public HisCareSumCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE_SUM>();
        }

        private BridgeDAO<HIS_CARE_SUM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
