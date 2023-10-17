using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSuimIndex
{
    partial class HisSuimIndexCheck : EntityBase
    {
        public HisSuimIndexCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUIM_INDEX>();
        }

        private BridgeDAO<HIS_SUIM_INDEX> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
