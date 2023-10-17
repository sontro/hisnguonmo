using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineService
{
    partial class HisMedicineServiceTruncate : EntityBase
    {
        public HisMedicineServiceTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_SERVICE>();
        }

        private BridgeDAO<HIS_MEDICINE_SERVICE> bridgeDAO;

        public bool Truncate(HIS_MEDICINE_SERVICE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDICINE_SERVICE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
