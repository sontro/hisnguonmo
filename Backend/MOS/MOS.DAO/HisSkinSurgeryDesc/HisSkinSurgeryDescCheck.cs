using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSkinSurgeryDesc
{
    partial class HisSkinSurgeryDescCheck : EntityBase
    {
        public HisSkinSurgeryDescCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SKIN_SURGERY_DESC>();
        }

        private BridgeDAO<HIS_SKIN_SURGERY_DESC> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
