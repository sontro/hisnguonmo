using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineTypeTut
{
    partial class HisMedicineTypeTutUpdate : EntityBase
    {
        public HisMedicineTypeTutUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_TYPE_TUT>();
        }

        private BridgeDAO<HIS_MEDICINE_TYPE_TUT> bridgeDAO;

        public bool Update(HIS_MEDICINE_TYPE_TUT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDICINE_TYPE_TUT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
