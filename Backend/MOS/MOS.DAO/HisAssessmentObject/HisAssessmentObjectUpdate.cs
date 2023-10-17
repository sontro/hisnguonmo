using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAssessmentObject
{
    partial class HisAssessmentObjectUpdate : EntityBase
    {
        public HisAssessmentObjectUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ASSESSMENT_OBJECT>();
        }

        private BridgeDAO<HIS_ASSESSMENT_OBJECT> bridgeDAO;

        public bool Update(HIS_ASSESSMENT_OBJECT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ASSESSMENT_OBJECT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
