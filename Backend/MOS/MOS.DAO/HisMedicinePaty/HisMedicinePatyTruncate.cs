using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicinePaty
{
    partial class HisMedicinePatyTruncate : EntityBase
    {
        public HisMedicinePatyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_PATY>();
        }

        private BridgeDAO<HIS_MEDICINE_PATY> bridgeDAO;

        public bool Truncate(HIS_MEDICINE_PATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDICINE_PATY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
