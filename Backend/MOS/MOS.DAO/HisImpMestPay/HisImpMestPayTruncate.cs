using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMestPay
{
    partial class HisImpMestPayTruncate : EntityBase
    {
        public HisImpMestPayTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_PAY>();
        }

        private BridgeDAO<HIS_IMP_MEST_PAY> bridgeDAO;

        public bool Truncate(HIS_IMP_MEST_PAY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_IMP_MEST_PAY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
