using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPatientType
{
    partial class HisMestPatientTypeTruncate : EntityBase
    {
        public HisMestPatientTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PATIENT_TYPE>();
        }

        private BridgeDAO<HIS_MEST_PATIENT_TYPE> bridgeDAO;

        public bool Truncate(HIS_MEST_PATIENT_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEST_PATIENT_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
