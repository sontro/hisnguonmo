using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientType
{
    partial class HisPatientTypeUpdate : EntityBase
    {
        public HisPatientTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE> bridgeDAO;

        public bool Update(HIS_PATIENT_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PATIENT_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
