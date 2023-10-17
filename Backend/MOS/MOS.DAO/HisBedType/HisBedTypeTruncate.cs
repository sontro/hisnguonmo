using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBedType
{
    partial class HisBedTypeTruncate : EntityBase
    {
        public HisBedTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED_TYPE>();
        }

        private BridgeDAO<HIS_BED_TYPE> bridgeDAO;

        public bool Truncate(HIS_BED_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BED_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
