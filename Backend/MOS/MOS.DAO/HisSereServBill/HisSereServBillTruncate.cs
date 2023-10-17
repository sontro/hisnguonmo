using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServBill
{
    partial class HisSereServBillTruncate : EntityBase
    {
        public HisSereServBillTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_BILL>();
        }

        private BridgeDAO<HIS_SERE_SERV_BILL> bridgeDAO;

        public bool Truncate(HIS_SERE_SERV_BILL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERE_SERV_BILL> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
