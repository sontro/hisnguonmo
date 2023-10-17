using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientCase
{
    partial class HisPatientCaseTruncate : EntityBase
    {
        public HisPatientCaseTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_CASE>();
        }

        private BridgeDAO<HIS_PATIENT_CASE> bridgeDAO;

        public bool Truncate(HIS_PATIENT_CASE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PATIENT_CASE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
