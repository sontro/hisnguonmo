using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentEndTypeExt
{
    partial class HisTreatmentEndTypeExtTruncate : EntityBase
    {
        public HisTreatmentEndTypeExtTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_END_TYPE_EXT>();
        }

        private BridgeDAO<HIS_TREATMENT_END_TYPE_EXT> bridgeDAO;

        public bool Truncate(HIS_TREATMENT_END_TYPE_EXT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TREATMENT_END_TYPE_EXT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
