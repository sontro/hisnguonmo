using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCaroDepartment
{
    partial class HisCaroDepartmentUpdate : EntityBase
    {
        public HisCaroDepartmentUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARO_DEPARTMENT>();
        }

        private BridgeDAO<HIS_CARO_DEPARTMENT> bridgeDAO;

        public bool Update(HIS_CARO_DEPARTMENT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CARO_DEPARTMENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
