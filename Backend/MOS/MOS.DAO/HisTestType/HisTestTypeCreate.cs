using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTestType
{
    partial class HisTestTypeCreate : EntityBase
    {
        public HisTestTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_TYPE>();
        }

        private BridgeDAO<HIS_TEST_TYPE> bridgeDAO;

        public bool Create(HIS_TEST_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TEST_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
