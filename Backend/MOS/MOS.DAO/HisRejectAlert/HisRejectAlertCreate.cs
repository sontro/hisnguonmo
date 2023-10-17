using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRejectAlert
{
    partial class HisRejectAlertCreate : EntityBase
    {
        public HisRejectAlertCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REJECT_ALERT>();
        }

        private BridgeDAO<HIS_REJECT_ALERT> bridgeDAO;

        public bool Create(HIS_REJECT_ALERT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_REJECT_ALERT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
