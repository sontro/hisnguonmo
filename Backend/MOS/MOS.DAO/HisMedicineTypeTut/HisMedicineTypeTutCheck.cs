using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMedicineTypeTut
{
    partial class HisMedicineTypeTutCheck : EntityBase
    {
        public HisMedicineTypeTutCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_TYPE_TUT>();
        }

        private BridgeDAO<HIS_MEDICINE_TYPE_TUT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
