using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineInteractive
{
    partial class HisMedicineInteractiveTruncate : EntityBase
    {
        public HisMedicineInteractiveTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_INTERACTIVE>();
        }

        private BridgeDAO<HIS_MEDICINE_INTERACTIVE> bridgeDAO;

        public bool Truncate(HIS_MEDICINE_INTERACTIVE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDICINE_INTERACTIVE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
