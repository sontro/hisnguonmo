using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMedicineService
{
    partial class HisMedicineServiceCheck : EntityBase
    {
        public HisMedicineServiceCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_SERVICE>();
        }

        private BridgeDAO<HIS_MEDICINE_SERVICE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
