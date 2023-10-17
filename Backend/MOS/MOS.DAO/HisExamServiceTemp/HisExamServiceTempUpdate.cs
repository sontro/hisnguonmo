using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExamServiceTemp
{
    partial class HisExamServiceTempUpdate : EntityBase
    {
        public HisExamServiceTempUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXAM_SERVICE_TEMP>();
        }

        private BridgeDAO<HIS_EXAM_SERVICE_TEMP> bridgeDAO;

        public bool Update(HIS_EXAM_SERVICE_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXAM_SERVICE_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
