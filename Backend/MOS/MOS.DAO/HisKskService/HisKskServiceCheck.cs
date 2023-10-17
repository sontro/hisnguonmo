using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisKskService
{
    partial class HisKskServiceCheck : EntityBase
    {
        public HisKskServiceCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_SERVICE>();
        }

        private BridgeDAO<HIS_KSK_SERVICE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
