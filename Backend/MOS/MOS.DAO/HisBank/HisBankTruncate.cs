using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBank
{
    partial class HisBankTruncate : EntityBase
    {
        public HisBankTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BANK>();
        }

        private BridgeDAO<HIS_BANK> bridgeDAO;

        public bool Truncate(HIS_BANK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BANK> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
