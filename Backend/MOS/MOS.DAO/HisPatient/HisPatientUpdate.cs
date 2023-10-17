using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatient
{
    partial class HisPatientUpdate : EntityBase
    {
        public HisPatientUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT>();
        }

        private BridgeDAO<HIS_PATIENT> bridgeDAO;

        public bool Update(HIS_PATIENT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PATIENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
