using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisFileType
{
    partial class HisFileTypeTruncate : EntityBase
    {
        public HisFileTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FILE_TYPE>();
        }

        private BridgeDAO<HIS_FILE_TYPE> bridgeDAO;

        public bool Truncate(HIS_FILE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_FILE_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
