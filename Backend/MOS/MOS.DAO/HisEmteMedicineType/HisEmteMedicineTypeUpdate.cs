using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmteMedicineType
{
    partial class HisEmteMedicineTypeUpdate : EntityBase
    {
        public HisEmteMedicineTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMTE_MEDICINE_TYPE>();
        }

        private BridgeDAO<HIS_EMTE_MEDICINE_TYPE> bridgeDAO;

        public bool Update(HIS_EMTE_MEDICINE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EMTE_MEDICINE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
