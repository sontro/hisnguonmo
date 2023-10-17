using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMestMetyDepa
{
    partial class HisMestMetyDepaCheck : EntityBase
    {
        public HisMestMetyDepaCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_METY_DEPA>();
        }

        private BridgeDAO<HIS_MEST_METY_DEPA> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
