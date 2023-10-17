using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRemuneration
{
    partial class HisRemunerationUpdate : EntityBase
    {
        public HisRemunerationUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REMUNERATION>();
        }

        private BridgeDAO<HIS_REMUNERATION> bridgeDAO;

        public bool Update(HIS_REMUNERATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_REMUNERATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
