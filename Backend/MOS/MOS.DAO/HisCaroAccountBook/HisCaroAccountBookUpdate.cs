using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCaroAccountBook
{
    partial class HisCaroAccountBookUpdate : EntityBase
    {
        public HisCaroAccountBookUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARO_ACCOUNT_BOOK>();
        }

        private BridgeDAO<HIS_CARO_ACCOUNT_BOOK> bridgeDAO;

        public bool Update(HIS_CARO_ACCOUNT_BOOK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CARO_ACCOUNT_BOOK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
