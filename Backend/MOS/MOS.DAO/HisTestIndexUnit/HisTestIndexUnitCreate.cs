using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTestIndexUnit
{
    partial class HisTestIndexUnitCreate : EntityBase
    {
        public HisTestIndexUnitCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_INDEX_UNIT>();
        }

        private BridgeDAO<HIS_TEST_INDEX_UNIT> bridgeDAO;

        public bool Create(HIS_TEST_INDEX_UNIT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TEST_INDEX_UNIT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
