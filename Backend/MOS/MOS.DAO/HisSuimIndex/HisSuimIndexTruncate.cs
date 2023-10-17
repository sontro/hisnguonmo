using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSuimIndex
{
    partial class HisSuimIndexTruncate : EntityBase
    {
        public HisSuimIndexTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUIM_INDEX>();
        }

        private BridgeDAO<HIS_SUIM_INDEX> bridgeDAO;

        public bool Truncate(HIS_SUIM_INDEX data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SUIM_INDEX> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
