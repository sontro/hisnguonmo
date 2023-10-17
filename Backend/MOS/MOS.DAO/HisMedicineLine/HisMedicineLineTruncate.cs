using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineLine
{
    partial class HisMedicineLineTruncate : EntityBase
    {
        public HisMedicineLineTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_LINE>();
        }

        private BridgeDAO<HIS_MEDICINE_LINE> bridgeDAO;

        public bool Truncate(HIS_MEDICINE_LINE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDICINE_LINE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
