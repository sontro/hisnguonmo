using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentType
{
    partial class HisTreatmentTypeTruncate : EntityBase
    {
        public HisTreatmentTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_TYPE>();
        }

        private BridgeDAO<HIS_TREATMENT_TYPE> bridgeDAO;

        public bool Truncate(HIS_TREATMENT_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TREATMENT_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
