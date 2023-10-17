using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBhytParam
{
    partial class HisBhytParamTruncate : EntityBase
    {
        public HisBhytParamTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BHYT_PARAM>();
        }

        private BridgeDAO<HIS_BHYT_PARAM> bridgeDAO;

        public bool Truncate(HIS_BHYT_PARAM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BHYT_PARAM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
