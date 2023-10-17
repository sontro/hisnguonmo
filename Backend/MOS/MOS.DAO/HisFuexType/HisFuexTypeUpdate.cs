using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisFuexType
{
    partial class HisFuexTypeUpdate : EntityBase
    {
        public HisFuexTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FUEX_TYPE>();
        }

        private BridgeDAO<HIS_FUEX_TYPE> bridgeDAO;

        public bool Update(HIS_FUEX_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_FUEX_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
