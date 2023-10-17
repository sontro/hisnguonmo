using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBornResult
{
    partial class HisBornResultUpdate : EntityBase
    {
        public HisBornResultUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BORN_RESULT>();
        }

        private BridgeDAO<HIS_BORN_RESULT> bridgeDAO;

        public bool Update(HIS_BORN_RESULT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BORN_RESULT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
