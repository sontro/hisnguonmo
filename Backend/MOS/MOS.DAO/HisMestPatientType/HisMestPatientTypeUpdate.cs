using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPatientType
{
    partial class HisMestPatientTypeUpdate : EntityBase
    {
        public HisMestPatientTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PATIENT_TYPE>();
        }

        private BridgeDAO<HIS_MEST_PATIENT_TYPE> bridgeDAO;

        public bool Update(HIS_MEST_PATIENT_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEST_PATIENT_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
