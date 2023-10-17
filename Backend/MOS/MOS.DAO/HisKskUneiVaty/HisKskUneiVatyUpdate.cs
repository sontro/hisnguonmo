using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskUneiVaty
{
    partial class HisKskUneiVatyUpdate : EntityBase
    {
        public HisKskUneiVatyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_UNEI_VATY>();
        }

        private BridgeDAO<HIS_KSK_UNEI_VATY> bridgeDAO;

        public bool Update(HIS_KSK_UNEI_VATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_KSK_UNEI_VATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
