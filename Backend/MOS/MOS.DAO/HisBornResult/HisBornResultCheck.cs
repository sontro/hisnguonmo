using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBornResult
{
    partial class HisBornResultCheck : EntityBase
    {
        public HisBornResultCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BORN_RESULT>();
        }

        private BridgeDAO<HIS_BORN_RESULT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
