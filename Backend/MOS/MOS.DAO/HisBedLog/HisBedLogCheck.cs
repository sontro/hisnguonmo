using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBedLog
{
    partial class HisBedLogCheck : EntityBase
    {
        public HisBedLogCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED_LOG>();
        }

        private BridgeDAO<HIS_BED_LOG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
