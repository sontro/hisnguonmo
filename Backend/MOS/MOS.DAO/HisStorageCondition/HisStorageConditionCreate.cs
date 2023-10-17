using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisStorageCondition
{
    partial class HisStorageConditionCreate : EntityBase
    {
        public HisStorageConditionCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_STORAGE_CONDITION>();
        }

        private BridgeDAO<HIS_STORAGE_CONDITION> bridgeDAO;

        public bool Create(HIS_STORAGE_CONDITION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_STORAGE_CONDITION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
