using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMachine
{
    partial class HisMachineUpdate : EntityBase
    {
        public HisMachineUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MACHINE>();
        }

        private BridgeDAO<HIS_MACHINE> bridgeDAO;

        public bool Update(HIS_MACHINE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MACHINE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
