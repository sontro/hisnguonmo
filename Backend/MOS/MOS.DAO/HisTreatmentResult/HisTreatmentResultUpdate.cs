using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentResult
{
    partial class HisTreatmentResultUpdate : EntityBase
    {
        public HisTreatmentResultUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_RESULT>();
        }

        private BridgeDAO<HIS_TREATMENT_RESULT> bridgeDAO;

        public bool Update(HIS_TREATMENT_RESULT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TREATMENT_RESULT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
