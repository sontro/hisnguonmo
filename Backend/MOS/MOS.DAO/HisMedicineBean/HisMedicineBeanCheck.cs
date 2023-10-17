using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMedicineBean
{
    partial class HisMedicineBeanCheck : EntityBase
    {
        public HisMedicineBeanCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_BEAN>();
        }

        private BridgeDAO<HIS_MEDICINE_BEAN> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
