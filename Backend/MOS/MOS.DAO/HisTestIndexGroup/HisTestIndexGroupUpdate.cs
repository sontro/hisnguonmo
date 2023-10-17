using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTestIndexGroup
{
    partial class HisTestIndexGroupUpdate : EntityBase
    {
        public HisTestIndexGroupUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_INDEX_GROUP>();
        }

        private BridgeDAO<HIS_TEST_INDEX_GROUP> bridgeDAO;

        public bool Update(HIS_TEST_INDEX_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TEST_INDEX_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
