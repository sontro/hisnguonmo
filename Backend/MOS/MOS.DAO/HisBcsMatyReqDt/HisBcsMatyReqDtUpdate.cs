using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBcsMatyReqDt
{
    partial class HisBcsMatyReqDtUpdate : EntityBase
    {
        public HisBcsMatyReqDtUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BCS_MATY_REQ_DT>();
        }

        private BridgeDAO<HIS_BCS_MATY_REQ_DT> bridgeDAO;

        public bool Update(HIS_BCS_MATY_REQ_DT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BCS_MATY_REQ_DT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
