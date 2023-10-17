using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatient
{
    partial class HisPatientTruncate : EntityBase
    {
        public HisPatientTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT>();
        }

        private BridgeDAO<HIS_PATIENT> bridgeDAO;

        public bool Truncate(HIS_PATIENT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PATIENT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
