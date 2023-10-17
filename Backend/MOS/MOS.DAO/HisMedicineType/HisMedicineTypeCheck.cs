using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMedicineType
{
    partial class HisMedicineTypeCheck : EntityBase
    {
        public HisMedicineTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_TYPE>();
        }

        private BridgeDAO<HIS_MEDICINE_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
