using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTranPatiReason
{
    partial class HisTranPatiReasonUpdate : EntityBase
    {
        public HisTranPatiReasonUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRAN_PATI_REASON>();
        }

        private BridgeDAO<HIS_TRAN_PATI_REASON> bridgeDAO;

        public bool Update(HIS_TRAN_PATI_REASON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TRAN_PATI_REASON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
