using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAssessmentObject
{
    partial class HisAssessmentObjectCreate : EntityBase
    {
        public HisAssessmentObjectCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ASSESSMENT_OBJECT>();
        }

        private BridgeDAO<HIS_ASSESSMENT_OBJECT> bridgeDAO;

        public bool Create(HIS_ASSESSMENT_OBJECT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ASSESSMENT_OBJECT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
