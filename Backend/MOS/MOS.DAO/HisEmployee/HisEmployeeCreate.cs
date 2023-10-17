using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEmployee
{
    partial class HisEmployeeCreate : EntityBase
    {
        public HisEmployeeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMPLOYEE>();
        }

        private BridgeDAO<HIS_EMPLOYEE> bridgeDAO;

        public bool Create(HIS_EMPLOYEE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EMPLOYEE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
