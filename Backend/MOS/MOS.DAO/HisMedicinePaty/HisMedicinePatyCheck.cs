using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMedicinePaty
{
    partial class HisMedicinePatyCheck : EntityBase
    {
        public HisMedicinePatyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_PATY>();
        }

        private BridgeDAO<HIS_MEDICINE_PATY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
