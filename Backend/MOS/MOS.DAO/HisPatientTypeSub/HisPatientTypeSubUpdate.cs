using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientTypeSub
{
    partial class HisPatientTypeSubUpdate : EntityBase
    {
        public HisPatientTypeSubUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE_SUB>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE_SUB> bridgeDAO;

        public bool Update(HIS_PATIENT_TYPE_SUB data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PATIENT_TYPE_SUB> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
