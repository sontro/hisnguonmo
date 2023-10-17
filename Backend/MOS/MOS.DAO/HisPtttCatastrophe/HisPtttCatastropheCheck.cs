using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPtttCatastrophe
{
    partial class HisPtttCatastropheCheck : EntityBase
    {
        public HisPtttCatastropheCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_CATASTROPHE>();
        }

        private BridgeDAO<HIS_PTTT_CATASTROPHE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
