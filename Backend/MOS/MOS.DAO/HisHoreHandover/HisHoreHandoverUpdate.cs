using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHoreHandover
{
    partial class HisHoreHandoverUpdate : EntityBase
    {
        public HisHoreHandoverUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HORE_HANDOVER>();
        }

        private BridgeDAO<HIS_HORE_HANDOVER> bridgeDAO;

        public bool Update(HIS_HORE_HANDOVER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_HORE_HANDOVER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
