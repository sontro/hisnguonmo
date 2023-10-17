using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServDeposit
{
    partial class HisSereServDepositTruncate : EntityBase
    {
        public HisSereServDepositTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_DEPOSIT>();
        }

        private BridgeDAO<HIS_SERE_SERV_DEPOSIT> bridgeDAO;

        public bool Truncate(HIS_SERE_SERV_DEPOSIT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERE_SERV_DEPOSIT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
