using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHoreHandoverStt
{
    partial class HisHoreHandoverSttUpdate : EntityBase
    {
        public HisHoreHandoverSttUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HORE_HANDOVER_STT>();
        }

        private BridgeDAO<HIS_HORE_HANDOVER_STT> bridgeDAO;

        public bool Update(HIS_HORE_HANDOVER_STT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_HORE_HANDOVER_STT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
