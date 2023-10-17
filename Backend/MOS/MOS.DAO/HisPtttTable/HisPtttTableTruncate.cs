using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttTable
{
    partial class HisPtttTableTruncate : EntityBase
    {
        public HisPtttTableTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_TABLE>();
        }

        private BridgeDAO<HIS_PTTT_TABLE> bridgeDAO;

        public bool Truncate(HIS_PTTT_TABLE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PTTT_TABLE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
