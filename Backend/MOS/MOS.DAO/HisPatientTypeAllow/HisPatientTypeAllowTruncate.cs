using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientTypeAllow
{
    partial class HisPatientTypeAllowTruncate : EntityBase
    {
        public HisPatientTypeAllowTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE_ALLOW>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE_ALLOW> bridgeDAO;

        public bool Truncate(HIS_PATIENT_TYPE_ALLOW data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PATIENT_TYPE_ALLOW> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
