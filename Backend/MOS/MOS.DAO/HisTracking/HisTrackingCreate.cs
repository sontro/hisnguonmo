using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTracking
{
    partial class HisTrackingCreate : EntityBase
    {
        public HisTrackingCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRACKING>();
        }

        private BridgeDAO<HIS_TRACKING> bridgeDAO;

        public bool Create(HIS_TRACKING data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TRACKING> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
