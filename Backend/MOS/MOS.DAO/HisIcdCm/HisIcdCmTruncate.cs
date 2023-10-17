using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisIcdCm
{
    partial class HisIcdCmTruncate : EntityBase
    {
        public HisIcdCmTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ICD_CM>();
        }

        private BridgeDAO<HIS_ICD_CM> bridgeDAO;

        public bool Truncate(HIS_ICD_CM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ICD_CM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
