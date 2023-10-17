using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMestMedicine
{
    partial class HisImpMestMedicineUpdate : EntityBase
    {
        public HisImpMestMedicineUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_MEDICINE>();
        }

        private BridgeDAO<HIS_IMP_MEST_MEDICINE> bridgeDAO;

        public bool Update(HIS_IMP_MEST_MEDICINE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_IMP_MEST_MEDICINE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
