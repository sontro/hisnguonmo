using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBornResult
{
    partial class HisBornResultTruncate : EntityBase
    {
        public HisBornResultTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BORN_RESULT>();
        }

        private BridgeDAO<HIS_BORN_RESULT> bridgeDAO;

        public bool Truncate(HIS_BORN_RESULT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BORN_RESULT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
