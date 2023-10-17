using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineTypeAcin
{
    partial class HisMedicineTypeAcinUpdate : EntityBase
    {
        public HisMedicineTypeAcinUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_TYPE_ACIN>();
        }

        private BridgeDAO<HIS_MEDICINE_TYPE_ACIN> bridgeDAO;

        public bool Update(HIS_MEDICINE_TYPE_ACIN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDICINE_TYPE_ACIN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
