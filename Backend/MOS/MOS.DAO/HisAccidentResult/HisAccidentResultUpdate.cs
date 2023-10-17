using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAccidentResult
{
    partial class HisAccidentResultUpdate : EntityBase
    {
        public HisAccidentResultUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_RESULT>();
        }

        private BridgeDAO<HIS_ACCIDENT_RESULT> bridgeDAO;

        public bool Update(HIS_ACCIDENT_RESULT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ACCIDENT_RESULT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
