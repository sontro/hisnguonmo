using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCareTemp
{
    partial class HisCareTempUpdate : EntityBase
    {
        public HisCareTempUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE_TEMP>();
        }

        private BridgeDAO<HIS_CARE_TEMP> bridgeDAO;

        public bool Update(HIS_CARE_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CARE_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
