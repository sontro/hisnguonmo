using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTrackingTemp
{
    partial class HisTrackingTempCreate : EntityBase
    {
        public HisTrackingTempCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRACKING_TEMP>();
        }

        private BridgeDAO<HIS_TRACKING_TEMP> bridgeDAO;

        public bool Create(HIS_TRACKING_TEMP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TRACKING_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
