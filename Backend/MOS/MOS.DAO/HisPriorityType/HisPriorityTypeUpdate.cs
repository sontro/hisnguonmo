using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPriorityType
{
    partial class HisPriorityTypeUpdate : EntityBase
    {
        public HisPriorityTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PRIORITY_TYPE>();
        }

        private BridgeDAO<HIS_PRIORITY_TYPE> bridgeDAO;

        public bool Update(HIS_PRIORITY_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PRIORITY_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
