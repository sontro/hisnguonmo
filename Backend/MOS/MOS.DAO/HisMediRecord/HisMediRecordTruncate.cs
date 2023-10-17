using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediRecord
{
    partial class HisMediRecordTruncate : EntityBase
    {
        public HisMediRecordTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_RECORD>();
        }

        private BridgeDAO<HIS_MEDI_RECORD> bridgeDAO;

        public bool Truncate(HIS_MEDI_RECORD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDI_RECORD> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
