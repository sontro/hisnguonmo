using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRationSumStt
{
    partial class HisRationSumSttUpdate : EntityBase
    {
        public HisRationSumSttUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_SUM_STT>();
        }

        private BridgeDAO<HIS_RATION_SUM_STT> bridgeDAO;

        public bool Update(HIS_RATION_SUM_STT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_RATION_SUM_STT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
