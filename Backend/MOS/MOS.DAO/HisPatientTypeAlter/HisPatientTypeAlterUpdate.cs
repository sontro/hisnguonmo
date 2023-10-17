using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientTypeAlter
{
    partial class HisPatientTypeAlterUpdate : EntityBase
    {
        public HisPatientTypeAlterUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE_ALTER>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE_ALTER> bridgeDAO;

        public bool Update(HIS_PATIENT_TYPE_ALTER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PATIENT_TYPE_ALTER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
