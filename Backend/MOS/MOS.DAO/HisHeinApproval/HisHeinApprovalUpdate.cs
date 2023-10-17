using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHeinApproval
{
    partial class HisHeinApprovalUpdate : EntityBase
    {
        public HisHeinApprovalUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HEIN_APPROVAL>();
        }

        private BridgeDAO<HIS_HEIN_APPROVAL> bridgeDAO;

        public bool Update(HIS_HEIN_APPROVAL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_HEIN_APPROVAL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
