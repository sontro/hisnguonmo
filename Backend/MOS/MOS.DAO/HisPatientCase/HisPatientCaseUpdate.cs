using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientCase
{
    partial class HisPatientCaseUpdate : EntityBase
    {
        public HisPatientCaseUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_CASE>();
        }

        private BridgeDAO<HIS_PATIENT_CASE> bridgeDAO;

        public bool Update(HIS_PATIENT_CASE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PATIENT_CASE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
