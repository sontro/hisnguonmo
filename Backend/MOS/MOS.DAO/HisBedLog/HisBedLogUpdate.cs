using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBedLog
{
    partial class HisBedLogUpdate : EntityBase
    {
        public HisBedLogUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED_LOG>();
        }

        private BridgeDAO<HIS_BED_LOG> bridgeDAO;

        public bool Update(HIS_BED_LOG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BED_LOG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
