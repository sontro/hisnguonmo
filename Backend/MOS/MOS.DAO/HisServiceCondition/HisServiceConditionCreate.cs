using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceCondition
{
    partial class HisServiceConditionCreate : EntityBase
    {
        public HisServiceConditionCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_CONDITION>();
        }

        private BridgeDAO<HIS_SERVICE_CONDITION> bridgeDAO;

        public bool Create(HIS_SERVICE_CONDITION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_CONDITION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
