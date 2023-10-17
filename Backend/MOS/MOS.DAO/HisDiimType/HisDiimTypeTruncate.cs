using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDiimType
{
    partial class HisDiimTypeTruncate : EntityBase
    {
        public HisDiimTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DIIM_TYPE>();
        }

        private BridgeDAO<HIS_DIIM_TYPE> bridgeDAO;

        public bool Truncate(HIS_DIIM_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DIIM_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
