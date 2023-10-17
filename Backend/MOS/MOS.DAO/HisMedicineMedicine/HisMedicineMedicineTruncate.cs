using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineMedicine
{
    partial class HisMedicineMedicineTruncate : EntityBase
    {
        public HisMedicineMedicineTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_MEDICINE>();
        }

        private BridgeDAO<HIS_MEDICINE_MEDICINE> bridgeDAO;

        public bool Truncate(HIS_MEDICINE_MEDICINE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDICINE_MEDICINE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
