using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPrepare
{
    partial class HisPrepareUpdate : EntityBase
    {
        public HisPrepareUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PREPARE>();
        }

        private BridgeDAO<HIS_PREPARE> bridgeDAO;

        public bool Update(HIS_PREPARE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PREPARE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
