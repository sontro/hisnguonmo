using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBornType
{
    partial class HisBornTypeTruncate : EntityBase
    {
        public HisBornTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BORN_TYPE>();
        }

        private BridgeDAO<HIS_BORN_TYPE> bridgeDAO;

        public bool Truncate(HIS_BORN_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BORN_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
