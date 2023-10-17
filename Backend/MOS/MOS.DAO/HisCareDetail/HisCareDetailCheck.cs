using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisCareDetail
{
    partial class HisCareDetailCheck : EntityBase
    {
        public HisCareDetailCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE_DETAIL>();
        }

        private BridgeDAO<HIS_CARE_DETAIL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
