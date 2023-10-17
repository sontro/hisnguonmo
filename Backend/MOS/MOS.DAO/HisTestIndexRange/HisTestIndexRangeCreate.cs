using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTestIndexRange
{
    partial class HisTestIndexRangeCreate : EntityBase
    {
        public HisTestIndexRangeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_INDEX_RANGE>();
        }

        private BridgeDAO<HIS_TEST_INDEX_RANGE> bridgeDAO;

        public bool Create(HIS_TEST_INDEX_RANGE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TEST_INDEX_RANGE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
