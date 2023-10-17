using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSourceMedicine
{
    partial class HisSourceMedicineUpdate : EntityBase
    {
        public HisSourceMedicineUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SOURCE_MEDICINE>();
        }

        private BridgeDAO<HIS_SOURCE_MEDICINE> bridgeDAO;

        public bool Update(HIS_SOURCE_MEDICINE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SOURCE_MEDICINE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
