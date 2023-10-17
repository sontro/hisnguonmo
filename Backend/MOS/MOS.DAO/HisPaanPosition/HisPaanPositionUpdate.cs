using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPaanPosition
{
    partial class HisPaanPositionUpdate : EntityBase
    {
        public HisPaanPositionUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PAAN_POSITION>();
        }

        private BridgeDAO<HIS_PAAN_POSITION> bridgeDAO;

        public bool Update(HIS_PAAN_POSITION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PAAN_POSITION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
