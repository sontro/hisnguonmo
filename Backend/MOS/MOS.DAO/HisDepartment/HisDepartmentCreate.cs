using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDepartment
{
    partial class HisDepartmentCreate : EntityBase
    {
        public HisDepartmentCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEPARTMENT>();
        }

        private BridgeDAO<HIS_DEPARTMENT> bridgeDAO;

        public bool Create(HIS_DEPARTMENT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DEPARTMENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
