using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMachineServMaty
{
    partial class HisMachineServMatyUpdate : EntityBase
    {
        public HisMachineServMatyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MACHINE_SERV_MATY>();
        }

        private BridgeDAO<HIS_MACHINE_SERV_MATY> bridgeDAO;

        public bool Update(HIS_MACHINE_SERV_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MACHINE_SERV_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
