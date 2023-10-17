using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisUserGroupTempDt
{
    partial class HisUserGroupTempDtCheck : EntityBase
    {
        public HisUserGroupTempDtCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_USER_GROUP_TEMP_DT>();
        }

        private BridgeDAO<HIS_USER_GROUP_TEMP_DT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
