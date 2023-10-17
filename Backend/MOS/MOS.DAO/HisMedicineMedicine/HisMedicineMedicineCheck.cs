using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMedicineMedicine
{
    partial class HisMedicineMedicineCheck : EntityBase
    {
        public HisMedicineMedicineCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_MEDICINE>();
        }

        private BridgeDAO<HIS_MEDICINE_MEDICINE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
