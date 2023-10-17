using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAlert
{
    partial class HisAlertCreate : EntityBase
    {
        public HisAlertCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ALERT>();
        }

        private BridgeDAO<HIS_ALERT> bridgeDAO;

        public bool Create(HIS_ALERT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ALERT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
