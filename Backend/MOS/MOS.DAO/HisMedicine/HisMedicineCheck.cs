using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMedicine
{
    partial class HisMedicineCheck : EntityBase
    {
        public HisMedicineCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE>();
        }

        private BridgeDAO<HIS_MEDICINE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
