using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskUnderEighteen
{
    partial class HisKskUnderEighteenUpdate : EntityBase
    {
        public HisKskUnderEighteenUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_UNDER_EIGHTEEN>();
        }

        private BridgeDAO<HIS_KSK_UNDER_EIGHTEEN> bridgeDAO;

        public bool Update(HIS_KSK_UNDER_EIGHTEEN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_KSK_UNDER_EIGHTEEN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
