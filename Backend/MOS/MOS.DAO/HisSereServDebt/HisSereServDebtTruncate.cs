using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServDebt
{
    partial class HisSereServDebtTruncate : EntityBase
    {
        public HisSereServDebtTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_DEBT>();
        }

        private BridgeDAO<HIS_SERE_SERV_DEBT> bridgeDAO;

        public bool Truncate(HIS_SERE_SERV_DEBT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERE_SERV_DEBT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
