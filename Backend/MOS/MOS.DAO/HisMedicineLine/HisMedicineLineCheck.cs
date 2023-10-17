using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMedicineLine
{
    partial class HisMedicineLineCheck : EntityBase
    {
        public HisMedicineLineCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_LINE>();
        }

        private BridgeDAO<HIS_MEDICINE_LINE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
