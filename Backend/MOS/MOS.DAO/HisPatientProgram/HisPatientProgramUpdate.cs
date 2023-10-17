using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientProgram
{
    partial class HisPatientProgramUpdate : EntityBase
    {
        public HisPatientProgramUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_PROGRAM>();
        }

        private BridgeDAO<HIS_PATIENT_PROGRAM> bridgeDAO;

        public bool Update(HIS_PATIENT_PROGRAM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PATIENT_PROGRAM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
