using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpUserTempDt
{
    partial class HisImpUserTempDtUpdate : EntityBase
    {
        public HisImpUserTempDtUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_USER_TEMP_DT>();
        }

        private BridgeDAO<HIS_IMP_USER_TEMP_DT> bridgeDAO;

        public bool Update(HIS_IMP_USER_TEMP_DT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_IMP_USER_TEMP_DT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
