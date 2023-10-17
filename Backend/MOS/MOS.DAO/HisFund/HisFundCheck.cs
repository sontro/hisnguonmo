using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisFund
{
    partial class HisFundCheck : EntityBase
    {
        public HisFundCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FUND>();
        }

        private BridgeDAO<HIS_FUND> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
