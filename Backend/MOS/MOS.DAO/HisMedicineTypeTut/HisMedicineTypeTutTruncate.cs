using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineTypeTut
{
    partial class HisMedicineTypeTutTruncate : EntityBase
    {
        public HisMedicineTypeTutTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_TYPE_TUT>();
        }

        private BridgeDAO<HIS_MEDICINE_TYPE_TUT> bridgeDAO;

        public bool Truncate(HIS_MEDICINE_TYPE_TUT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDICINE_TYPE_TUT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
