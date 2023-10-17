using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPtttMethod
{
    partial class HisPtttMethodCheck : EntityBase
    {
        public HisPtttMethodCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_METHOD>();
        }

        private BridgeDAO<HIS_PTTT_METHOD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
