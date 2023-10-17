using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpUserTemp
{
    partial class HisImpUserTempUpdate : EntityBase
    {
        public HisImpUserTempUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_USER_TEMP>();
        }

        private BridgeDAO<HIS_IMP_USER_TEMP> bridgeDAO;

        public bool Update(HIS_IMP_USER_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_IMP_USER_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
