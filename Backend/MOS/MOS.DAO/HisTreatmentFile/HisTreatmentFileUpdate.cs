using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentFile
{
    partial class HisTreatmentFileUpdate : EntityBase
    {
        public HisTreatmentFileUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_FILE>();
        }

        private BridgeDAO<HIS_TREATMENT_FILE> bridgeDAO;

        public bool Update(HIS_TREATMENT_FILE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TREATMENT_FILE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
