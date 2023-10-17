using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisHoreHoha
{
    partial class HisHoreHohaCheck : EntityBase
    {
        public HisHoreHohaCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HORE_HOHA>();
        }

        private BridgeDAO<HIS_HORE_HOHA> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
