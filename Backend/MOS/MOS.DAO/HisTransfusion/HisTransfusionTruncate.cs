using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTransfusion
{
    partial class HisTransfusionTruncate : EntityBase
    {
        public HisTransfusionTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSFUSION>();
        }

        private BridgeDAO<HIS_TRANSFUSION> bridgeDAO;

        public bool Truncate(HIS_TRANSFUSION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TRANSFUSION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
