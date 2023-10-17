using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServBill
{
    partial class HisSereServBillUpdate : EntityBase
    {
        public HisSereServBillUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_BILL>();
        }

        private BridgeDAO<HIS_SERE_SERV_BILL> bridgeDAO;

        public bool Update(HIS_SERE_SERV_BILL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERE_SERV_BILL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
