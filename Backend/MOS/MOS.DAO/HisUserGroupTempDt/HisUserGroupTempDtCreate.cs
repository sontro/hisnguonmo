using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisUserGroupTempDt
{
    partial class HisUserGroupTempDtCreate : EntityBase
    {
        public HisUserGroupTempDtCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_USER_GROUP_TEMP_DT>();
        }

        private BridgeDAO<HIS_USER_GROUP_TEMP_DT> bridgeDAO;

        public bool Create(HIS_USER_GROUP_TEMP_DT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_USER_GROUP_TEMP_DT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
