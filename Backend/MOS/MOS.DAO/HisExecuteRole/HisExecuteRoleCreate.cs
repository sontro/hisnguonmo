using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExecuteRole
{
    partial class HisExecuteRoleCreate : EntityBase
    {
        public HisExecuteRoleCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXECUTE_ROLE>();
        }

        private BridgeDAO<HIS_EXECUTE_ROLE> bridgeDAO;

        public bool Create(HIS_EXECUTE_ROLE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXECUTE_ROLE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
