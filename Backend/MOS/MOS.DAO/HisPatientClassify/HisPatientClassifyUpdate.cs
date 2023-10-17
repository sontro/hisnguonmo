using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientClassify
{
    partial class HisPatientClassifyUpdate : EntityBase
    {
        public HisPatientClassifyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_CLASSIFY>();
        }

        private BridgeDAO<HIS_PATIENT_CLASSIFY> bridgeDAO;

        public bool Update(HIS_PATIENT_CLASSIFY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PATIENT_CLASSIFY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
