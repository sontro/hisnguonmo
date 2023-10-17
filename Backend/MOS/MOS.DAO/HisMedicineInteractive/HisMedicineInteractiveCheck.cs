using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMedicineInteractive
{
    partial class HisMedicineInteractiveCheck : EntityBase
    {
        public HisMedicineInteractiveCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_INTERACTIVE>();
        }

        private BridgeDAO<HIS_MEDICINE_INTERACTIVE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
