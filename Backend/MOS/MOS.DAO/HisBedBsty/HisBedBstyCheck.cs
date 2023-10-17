using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBedBsty
{
    partial class HisBedBstyCheck : EntityBase
    {
        public HisBedBstyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED_BSTY>();
        }

        private BridgeDAO<HIS_BED_BSTY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
