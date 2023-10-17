using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTestSampleType
{
    partial class HisTestSampleTypeCreate : EntityBase
    {
        public HisTestSampleTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_SAMPLE_TYPE>();
        }

        private BridgeDAO<HIS_TEST_SAMPLE_TYPE> bridgeDAO;

        public bool Create(HIS_TEST_SAMPLE_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TEST_SAMPLE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
