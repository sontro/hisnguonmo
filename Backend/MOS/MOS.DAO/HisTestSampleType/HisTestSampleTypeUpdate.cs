using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTestSampleType
{
    partial class HisTestSampleTypeUpdate : EntityBase
    {
        public HisTestSampleTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_SAMPLE_TYPE>();
        }

        private BridgeDAO<HIS_TEST_SAMPLE_TYPE> bridgeDAO;

        public bool Update(HIS_TEST_SAMPLE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TEST_SAMPLE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
