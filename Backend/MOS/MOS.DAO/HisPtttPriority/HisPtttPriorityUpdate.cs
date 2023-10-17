using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttPriority
{
    partial class HisPtttPriorityUpdate : EntityBase
    {
        public HisPtttPriorityUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_PRIORITY>();
        }

        private BridgeDAO<HIS_PTTT_PRIORITY> bridgeDAO;

        public bool Update(HIS_PTTT_PRIORITY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PTTT_PRIORITY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
