using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCare
{
    partial class HisCareUpdate : EntityBase
    {
        public HisCareUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE>();
        }

        private BridgeDAO<HIS_CARE> bridgeDAO;

        public bool Update(HIS_CARE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CARE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
