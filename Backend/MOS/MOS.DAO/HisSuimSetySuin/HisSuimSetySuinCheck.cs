using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSuimSetySuin
{
    partial class HisSuimSetySuinCheck : EntityBase
    {
        public HisSuimSetySuinCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUIM_SETY_SUIN>();
        }

        private BridgeDAO<HIS_SUIM_SETY_SUIN> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
