using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisCaroDepartment
{
    partial class HisCaroDepartmentCreate : EntityBase
    {
        public HisCaroDepartmentCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARO_DEPARTMENT>();
        }

        private BridgeDAO<HIS_CARO_DEPARTMENT> bridgeDAO;

        public bool Create(HIS_CARO_DEPARTMENT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CARO_DEPARTMENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
