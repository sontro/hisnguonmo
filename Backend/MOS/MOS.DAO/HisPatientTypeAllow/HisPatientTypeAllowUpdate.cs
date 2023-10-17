using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientTypeAllow
{
    partial class HisPatientTypeAllowUpdate : EntityBase
    {
        public HisPatientTypeAllowUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE_ALLOW>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE_ALLOW> bridgeDAO;

        public bool Update(HIS_PATIENT_TYPE_ALLOW data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PATIENT_TYPE_ALLOW> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
