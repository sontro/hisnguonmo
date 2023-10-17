using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRehaSum
{
    partial class HisRehaSumUpdate : EntityBase
    {
        public HisRehaSumUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REHA_SUM>();
        }

        private BridgeDAO<HIS_REHA_SUM> bridgeDAO;

        public bool Update(HIS_REHA_SUM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_REHA_SUM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
