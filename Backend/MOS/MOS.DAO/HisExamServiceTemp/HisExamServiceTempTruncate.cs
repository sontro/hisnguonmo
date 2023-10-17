using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExamServiceTemp
{
    partial class HisExamServiceTempTruncate : EntityBase
    {
        public HisExamServiceTempTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXAM_SERVICE_TEMP>();
        }

        private BridgeDAO<HIS_EXAM_SERVICE_TEMP> bridgeDAO;

        public bool Truncate(HIS_EXAM_SERVICE_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXAM_SERVICE_TEMP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
