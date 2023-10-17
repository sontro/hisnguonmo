using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTestIndex
{
    partial class HisTestIndexCreate : EntityBase
    {
        public HisTestIndexCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_INDEX>();
        }

        private BridgeDAO<HIS_TEST_INDEX> bridgeDAO;

        public bool Create(HIS_TEST_INDEX data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TEST_INDEX> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
