using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExecuteGroup
{
    partial class HisExecuteGroupCreate : EntityBase
    {
        public HisExecuteGroupCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXECUTE_GROUP>();
        }

        private BridgeDAO<HIS_EXECUTE_GROUP> bridgeDAO;

        public bool Create(HIS_EXECUTE_GROUP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXECUTE_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
