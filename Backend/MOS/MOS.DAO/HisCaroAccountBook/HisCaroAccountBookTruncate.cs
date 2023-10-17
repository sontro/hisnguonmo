using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCaroAccountBook
{
    partial class HisCaroAccountBookTruncate : EntityBase
    {
        public HisCaroAccountBookTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARO_ACCOUNT_BOOK>();
        }

        private BridgeDAO<HIS_CARO_ACCOUNT_BOOK> bridgeDAO;

        public bool Truncate(HIS_CARO_ACCOUNT_BOOK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CARO_ACCOUNT_BOOK> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
