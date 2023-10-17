using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisUserGroupTemp
{
    partial class HisUserGroupTempCreate : EntityBase
    {
        public HisUserGroupTempCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_USER_GROUP_TEMP>();
        }

        private BridgeDAO<HIS_USER_GROUP_TEMP> bridgeDAO;

        public bool Create(HIS_USER_GROUP_TEMP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_USER_GROUP_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
