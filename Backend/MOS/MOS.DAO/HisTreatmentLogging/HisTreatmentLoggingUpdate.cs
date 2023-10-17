using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentLogging
{
    partial class HisTreatmentLoggingUpdate : EntityBase
    {
        public HisTreatmentLoggingUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_LOGGING>();
        }

        private BridgeDAO<HIS_TREATMENT_LOGGING> bridgeDAO;

        public bool Update(HIS_TREATMENT_LOGGING data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TREATMENT_LOGGING> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
