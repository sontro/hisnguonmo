using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisCare
{
    partial class HisCareCheck : EntityBase
    {
        public HisCareCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE>();
        }

        private BridgeDAO<HIS_CARE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
