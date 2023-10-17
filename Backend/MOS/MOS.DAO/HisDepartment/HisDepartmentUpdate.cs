using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDepartment
{
    partial class HisDepartmentUpdate : EntityBase
    {
        public HisDepartmentUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEPARTMENT>();
        }

        private BridgeDAO<HIS_DEPARTMENT> bridgeDAO;

        public bool Update(HIS_DEPARTMENT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DEPARTMENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
