using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTestSampleType
{
    partial class HisTestSampleTypeCheck : EntityBase
    {
        public HisTestSampleTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_SAMPLE_TYPE>();
        }

        private BridgeDAO<HIS_TEST_SAMPLE_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
