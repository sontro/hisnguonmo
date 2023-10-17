using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBornType
{
    partial class HisBornTypeCheck : EntityBase
    {
        public HisBornTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BORN_TYPE>();
        }

        private BridgeDAO<HIS_BORN_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
