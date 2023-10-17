using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExamServiceTemp
{
    partial class HisExamServiceTempCreate : EntityBase
    {
        public HisExamServiceTempCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXAM_SERVICE_TEMP>();
        }

        private BridgeDAO<HIS_EXAM_SERVICE_TEMP> bridgeDAO;

        public bool Create(HIS_EXAM_SERVICE_TEMP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXAM_SERVICE_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
