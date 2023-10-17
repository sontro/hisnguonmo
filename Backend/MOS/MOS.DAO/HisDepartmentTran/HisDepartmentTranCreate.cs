using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDepartmentTran
{
    partial class HisDepartmentTranCreate : EntityBase
    {
        public HisDepartmentTranCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEPARTMENT_TRAN>();
        }

        private BridgeDAO<HIS_DEPARTMENT_TRAN> bridgeDAO;

        public bool Create(HIS_DEPARTMENT_TRAN data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DEPARTMENT_TRAN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
