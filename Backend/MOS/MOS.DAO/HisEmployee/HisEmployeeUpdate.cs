using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmployee
{
    partial class HisEmployeeUpdate : EntityBase
    {
        public HisEmployeeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMPLOYEE>();
        }

        private BridgeDAO<HIS_EMPLOYEE> bridgeDAO;

        public bool Update(HIS_EMPLOYEE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EMPLOYEE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
