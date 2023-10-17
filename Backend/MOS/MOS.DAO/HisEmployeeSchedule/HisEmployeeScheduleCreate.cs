using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEmployeeSchedule
{
    partial class HisEmployeeScheduleCreate : EntityBase
    {
        public HisEmployeeScheduleCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMPLOYEE_SCHEDULE>();
        }

        private BridgeDAO<HIS_EMPLOYEE_SCHEDULE> bridgeDAO;

        public bool Create(HIS_EMPLOYEE_SCHEDULE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EMPLOYEE_SCHEDULE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
