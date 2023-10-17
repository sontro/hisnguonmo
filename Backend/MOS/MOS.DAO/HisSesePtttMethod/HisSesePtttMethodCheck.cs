using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSesePtttMethod
{
    partial class HisSesePtttMethodCheck : EntityBase
    {
        public HisSesePtttMethodCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SESE_PTTT_METHOD>();
        }

        private BridgeDAO<HIS_SESE_PTTT_METHOD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
