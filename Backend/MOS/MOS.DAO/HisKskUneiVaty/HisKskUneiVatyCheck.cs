using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisKskUneiVaty
{
    partial class HisKskUneiVatyCheck : EntityBase
    {
        public HisKskUneiVatyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_UNEI_VATY>();
        }

        private BridgeDAO<HIS_KSK_UNEI_VATY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
