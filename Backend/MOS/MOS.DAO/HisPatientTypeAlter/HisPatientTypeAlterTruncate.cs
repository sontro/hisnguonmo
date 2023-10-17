using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientTypeAlter
{
    partial class HisPatientTypeAlterTruncate : EntityBase
    {
        public HisPatientTypeAlterTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE_ALTER>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE_ALTER> bridgeDAO;

        public bool Truncate(HIS_PATIENT_TYPE_ALTER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PATIENT_TYPE_ALTER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
