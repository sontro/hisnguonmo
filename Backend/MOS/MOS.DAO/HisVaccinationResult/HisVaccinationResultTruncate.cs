using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccinationResult
{
    partial class HisVaccinationResultTruncate : EntityBase
    {
        public HisVaccinationResultTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_RESULT>();
        }

        private BridgeDAO<HIS_VACCINATION_RESULT> bridgeDAO;

        public bool Truncate(HIS_VACCINATION_RESULT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_VACCINATION_RESULT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
