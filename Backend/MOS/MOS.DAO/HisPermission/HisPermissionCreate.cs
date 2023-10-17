using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPermission
{
    partial class HisPermissionCreate : EntityBase
    {
        public HisPermissionCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PERMISSION>();
        }

        private BridgeDAO<HIS_PERMISSION> bridgeDAO;

        public bool Create(HIS_PERMISSION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PERMISSION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
