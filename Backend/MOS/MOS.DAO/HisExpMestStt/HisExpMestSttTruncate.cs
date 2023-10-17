using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMestStt
{
    partial class HisExpMestSttTruncate : EntityBase
    {
        public HisExpMestSttTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_STT>();
        }

        private BridgeDAO<HIS_EXP_MEST_STT> bridgeDAO;

        public bool Truncate(HIS_EXP_MEST_STT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXP_MEST_STT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
