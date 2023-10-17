using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBornType
{
    partial class HisBornTypeUpdate : EntityBase
    {
        public HisBornTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BORN_TYPE>();
        }

        private BridgeDAO<HIS_BORN_TYPE> bridgeDAO;

        public bool Update(HIS_BORN_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BORN_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
