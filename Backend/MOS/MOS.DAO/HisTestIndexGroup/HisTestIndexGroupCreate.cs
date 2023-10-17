using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTestIndexGroup
{
    partial class HisTestIndexGroupCreate : EntityBase
    {
        public HisTestIndexGroupCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_INDEX_GROUP>();
        }

        private BridgeDAO<HIS_TEST_INDEX_GROUP> bridgeDAO;

        public bool Create(HIS_TEST_INDEX_GROUP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TEST_INDEX_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
