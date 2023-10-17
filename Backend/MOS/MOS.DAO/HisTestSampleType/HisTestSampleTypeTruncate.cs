using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTestSampleType
{
    partial class HisTestSampleTypeTruncate : EntityBase
    {
        public HisTestSampleTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_SAMPLE_TYPE>();
        }

        private BridgeDAO<HIS_TEST_SAMPLE_TYPE> bridgeDAO;

        public bool Truncate(HIS_TEST_SAMPLE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TEST_SAMPLE_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
