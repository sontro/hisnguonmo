using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskDriver
{
    partial class HisKskDriverUpdate : EntityBase
    {
        public HisKskDriverUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_DRIVER>();
        }

        private BridgeDAO<HIS_KSK_DRIVER> bridgeDAO;

        public bool Update(HIS_KSK_DRIVER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_KSK_DRIVER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
