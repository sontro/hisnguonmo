using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttCondition
{
    partial class HisPtttConditionUpdate : EntityBase
    {
        public HisPtttConditionUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_CONDITION>();
        }

        private BridgeDAO<HIS_PTTT_CONDITION> bridgeDAO;

        public bool Update(HIS_PTTT_CONDITION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PTTT_CONDITION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
