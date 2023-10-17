using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHospitalizeReason
{
    partial class HisHospitalizeReasonUpdate : EntityBase
    {
        public HisHospitalizeReasonUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HOSPITALIZE_REASON>();
        }

        private BridgeDAO<HIS_HOSPITALIZE_REASON> bridgeDAO;

        public bool Update(HIS_HOSPITALIZE_REASON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_HOSPITALIZE_REASON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
