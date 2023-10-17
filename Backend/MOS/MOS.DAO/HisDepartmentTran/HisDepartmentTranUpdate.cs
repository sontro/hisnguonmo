using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDepartmentTran
{
    partial class HisDepartmentTranUpdate : EntityBase
    {
        public HisDepartmentTranUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEPARTMENT_TRAN>();
        }

        private BridgeDAO<HIS_DEPARTMENT_TRAN> bridgeDAO;

        public bool Update(HIS_DEPARTMENT_TRAN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DEPARTMENT_TRAN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
