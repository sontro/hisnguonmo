using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPtttTable
{
    partial class HisPtttTableCheck : EntityBase
    {
        public HisPtttTableCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_TABLE>();
        }

        private BridgeDAO<HIS_PTTT_TABLE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
