using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisFuexType
{
    partial class HisFuexTypeCheck : EntityBase
    {
        public HisFuexTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FUEX_TYPE>();
        }

        private BridgeDAO<HIS_FUEX_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
