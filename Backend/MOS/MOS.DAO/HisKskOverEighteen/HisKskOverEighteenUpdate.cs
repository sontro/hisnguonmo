using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskOverEighteen
{
    partial class HisKskOverEighteenUpdate : EntityBase
    {
        public HisKskOverEighteenUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_OVER_EIGHTEEN>();
        }

        private BridgeDAO<HIS_KSK_OVER_EIGHTEEN> bridgeDAO;

        public bool Update(HIS_KSK_OVER_EIGHTEEN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_KSK_OVER_EIGHTEEN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
