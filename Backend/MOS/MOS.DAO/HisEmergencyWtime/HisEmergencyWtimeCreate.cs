using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEmergencyWtime
{
    partial class HisEmergencyWtimeCreate : EntityBase
    {
        public HisEmergencyWtimeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMERGENCY_WTIME>();
        }

        private BridgeDAO<HIS_EMERGENCY_WTIME> bridgeDAO;

        public bool Create(HIS_EMERGENCY_WTIME data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EMERGENCY_WTIME> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
