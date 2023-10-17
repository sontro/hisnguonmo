using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExamSchedule
{
    partial class HisExamScheduleCreate : EntityBase
    {
        public HisExamScheduleCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXAM_SCHEDULE>();
        }

        private BridgeDAO<HIS_EXAM_SCHEDULE> bridgeDAO;

        public bool Create(HIS_EXAM_SCHEDULE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXAM_SCHEDULE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
