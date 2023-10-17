using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExecuteRoleUser
{
    partial class HisExecuteRoleUserCreate : EntityBase
    {
        public HisExecuteRoleUserCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXECUTE_ROLE_USER>();
        }

        private BridgeDAO<HIS_EXECUTE_ROLE_USER> bridgeDAO;

        public bool Create(HIS_EXECUTE_ROLE_USER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXECUTE_ROLE_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
