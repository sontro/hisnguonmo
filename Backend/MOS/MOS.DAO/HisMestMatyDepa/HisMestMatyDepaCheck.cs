using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMestMatyDepa
{
    partial class HisMestMatyDepaCheck : EntityBase
    {
        public HisMestMatyDepaCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_MATY_DEPA>();
        }

        private BridgeDAO<HIS_MEST_MATY_DEPA> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
