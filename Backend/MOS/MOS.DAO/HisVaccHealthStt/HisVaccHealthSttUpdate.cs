using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccHealthStt
{
    partial class HisVaccHealthSttUpdate : EntityBase
    {
        public HisVaccHealthSttUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_HEALTH_STT>();
        }

        private BridgeDAO<HIS_VACC_HEALTH_STT> bridgeDAO;

        public bool Update(HIS_VACC_HEALTH_STT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_VACC_HEALTH_STT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
