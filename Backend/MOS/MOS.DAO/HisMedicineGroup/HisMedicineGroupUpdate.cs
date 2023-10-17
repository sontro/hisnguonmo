using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineGroup
{
    partial class HisMedicineGroupUpdate : EntityBase
    {
        public HisMedicineGroupUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_GROUP>();
        }

        private BridgeDAO<HIS_MEDICINE_GROUP> bridgeDAO;

        public bool Update(HIS_MEDICINE_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDICINE_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
