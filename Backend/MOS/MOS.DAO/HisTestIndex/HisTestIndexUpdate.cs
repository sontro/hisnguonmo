using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTestIndex
{
    partial class HisTestIndexUpdate : EntityBase
    {
        public HisTestIndexUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_INDEX>();
        }

        private BridgeDAO<HIS_TEST_INDEX> bridgeDAO;

        public bool Update(HIS_TEST_INDEX data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TEST_INDEX> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
