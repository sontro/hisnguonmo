using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceMachine
{
    partial class HisServiceMachineUpdate : EntityBase
    {
        public HisServiceMachineUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_MACHINE>();
        }

        private BridgeDAO<HIS_SERVICE_MACHINE> bridgeDAO;

        public bool Update(HIS_SERVICE_MACHINE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_MACHINE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
