using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDhst
{
    partial class HisDhstCheck : EntityBase
    {
        public HisDhstCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DHST>();
        }

        private BridgeDAO<HIS_DHST> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
