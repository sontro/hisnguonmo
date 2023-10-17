using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientClassify
{
    partial class HisPatientClassifyTruncate : EntityBase
    {
        public HisPatientClassifyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_CLASSIFY>();
        }

        private BridgeDAO<HIS_PATIENT_CLASSIFY> bridgeDAO;

        public bool Truncate(HIS_PATIENT_CLASSIFY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PATIENT_CLASSIFY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
