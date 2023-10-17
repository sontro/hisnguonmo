using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSeseDepoRepay
{
    partial class HisSeseDepoRepayUpdate : EntityBase
    {
        public HisSeseDepoRepayUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SESE_DEPO_REPAY>();
        }

        private BridgeDAO<HIS_SESE_DEPO_REPAY> bridgeDAO;

        public bool Update(HIS_SESE_DEPO_REPAY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SESE_DEPO_REPAY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
