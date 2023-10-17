using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientProgram
{
    partial class HisPatientProgramTruncate : EntityBase
    {
        public HisPatientProgramTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_PROGRAM>();
        }

        private BridgeDAO<HIS_PATIENT_PROGRAM> bridgeDAO;

        public bool Truncate(HIS_PATIENT_PROGRAM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PATIENT_PROGRAM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
