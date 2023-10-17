using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisKskGeneral
{
    partial class HisKskGeneralCheck : EntityBase
    {
        public HisKskGeneralCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_GENERAL>();
        }

        private BridgeDAO<HIS_KSK_GENERAL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
