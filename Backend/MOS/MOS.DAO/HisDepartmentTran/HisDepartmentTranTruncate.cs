using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDepartmentTran
{
    partial class HisDepartmentTranTruncate : EntityBase
    {
        public HisDepartmentTranTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEPARTMENT_TRAN>();
        }

        private BridgeDAO<HIS_DEPARTMENT_TRAN> bridgeDAO;

        public bool Truncate(HIS_DEPARTMENT_TRAN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DEPARTMENT_TRAN> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
