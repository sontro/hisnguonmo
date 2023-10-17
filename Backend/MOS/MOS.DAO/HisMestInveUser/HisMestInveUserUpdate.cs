using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestInveUser
{
    partial class HisMestInveUserUpdate : EntityBase
    {
        public HisMestInveUserUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_INVE_USER>();
        }

        private BridgeDAO<HIS_MEST_INVE_USER> bridgeDAO;

        public bool Update(HIS_MEST_INVE_USER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEST_INVE_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
