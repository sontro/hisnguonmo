using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExamSereDire
{
    partial class HisExamSereDireTruncate : EntityBase
    {
        public HisExamSereDireTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXAM_SERE_DIRE>();
        }

        private BridgeDAO<HIS_EXAM_SERE_DIRE> bridgeDAO;

        public bool Truncate(HIS_EXAM_SERE_DIRE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXAM_SERE_DIRE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
