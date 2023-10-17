using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccinationExam
{
    partial class HisVaccinationExamTruncate : EntityBase
    {
        public HisVaccinationExamTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_EXAM>();
        }

        private BridgeDAO<HIS_VACCINATION_EXAM> bridgeDAO;

        public bool Truncate(HIS_VACCINATION_EXAM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_VACCINATION_EXAM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
