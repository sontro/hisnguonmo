using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDocHoldType
{
    partial class HisDocHoldTypeUpdate : EntityBase
    {
        public HisDocHoldTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DOC_HOLD_TYPE>();
        }

        private BridgeDAO<HIS_DOC_HOLD_TYPE> bridgeDAO;

        public bool Update(HIS_DOC_HOLD_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DOC_HOLD_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
