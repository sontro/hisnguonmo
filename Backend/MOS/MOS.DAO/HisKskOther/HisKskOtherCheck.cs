using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisKskOther
{
    partial class HisKskOtherCheck : EntityBase
    {
        public HisKskOtherCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_OTHER>();
        }

        private BridgeDAO<HIS_KSK_OTHER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
