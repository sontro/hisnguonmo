using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMestPatySub
{
    partial class HisMestPatySubCheck : EntityBase
    {
        public HisMestPatySubCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PATY_SUB>();
        }

        private BridgeDAO<HIS_MEST_PATY_SUB> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
