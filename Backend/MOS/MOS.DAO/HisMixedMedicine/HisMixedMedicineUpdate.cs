using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMixedMedicine
{
    partial class HisMixedMedicineUpdate : EntityBase
    {
        public HisMixedMedicineUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MIXED_MEDICINE>();
        }

        private BridgeDAO<HIS_MIXED_MEDICINE> bridgeDAO;

        public bool Update(HIS_MIXED_MEDICINE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MIXED_MEDICINE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
