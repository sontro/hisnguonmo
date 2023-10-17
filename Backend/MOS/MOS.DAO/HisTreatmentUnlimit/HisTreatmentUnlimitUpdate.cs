using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentUnlimit
{
    partial class HisTreatmentUnlimitUpdate : EntityBase
    {
        public HisTreatmentUnlimitUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_UNLIMIT>();
        }

        private BridgeDAO<HIS_TREATMENT_UNLIMIT> bridgeDAO;

        public bool Update(HIS_TREATMENT_UNLIMIT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TREATMENT_UNLIMIT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
