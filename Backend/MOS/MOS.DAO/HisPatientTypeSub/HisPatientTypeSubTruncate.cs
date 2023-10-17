using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientTypeSub
{
    partial class HisPatientTypeSubTruncate : EntityBase
    {
        public HisPatientTypeSubTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE_SUB>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE_SUB> bridgeDAO;

        public bool Truncate(HIS_PATIENT_TYPE_SUB data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PATIENT_TYPE_SUB> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
