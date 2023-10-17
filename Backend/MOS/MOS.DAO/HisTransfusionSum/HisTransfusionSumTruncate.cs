using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTransfusionSum
{
    partial class HisTransfusionSumTruncate : EntityBase
    {
        public HisTransfusionSumTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSFUSION_SUM>();
        }

        private BridgeDAO<HIS_TRANSFUSION_SUM> bridgeDAO;

        public bool Truncate(HIS_TRANSFUSION_SUM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TRANSFUSION_SUM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
