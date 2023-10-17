using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisVareVart
{
    partial class HisVareVartCheck : EntityBase
    {
        public HisVareVartCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VARE_VART>();
        }

        private BridgeDAO<HIS_VARE_VART> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
