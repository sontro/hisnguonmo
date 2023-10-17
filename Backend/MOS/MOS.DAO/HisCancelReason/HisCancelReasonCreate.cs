using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisCancelReason
{
    partial class HisCancelReasonCreate : EntityBase
    {
        public HisCancelReasonCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CANCEL_REASON>();
        }

        private BridgeDAO<HIS_CANCEL_REASON> bridgeDAO;

        public bool Create(HIS_CANCEL_REASON data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CANCEL_REASON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
