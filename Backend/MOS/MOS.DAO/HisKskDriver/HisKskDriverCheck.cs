using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisKskDriver
{
    partial class HisKskDriverCheck : EntityBase
    {
        public HisKskDriverCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_DRIVER>();
        }

        private BridgeDAO<HIS_KSK_DRIVER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
