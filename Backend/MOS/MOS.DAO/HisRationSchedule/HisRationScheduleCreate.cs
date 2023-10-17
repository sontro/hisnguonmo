using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRationSchedule
{
    partial class HisRationScheduleCreate : EntityBase
    {
        public HisRationScheduleCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_SCHEDULE>();
        }

        private BridgeDAO<HIS_RATION_SCHEDULE> bridgeDAO;

        public bool Create(HIS_RATION_SCHEDULE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_RATION_SCHEDULE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
