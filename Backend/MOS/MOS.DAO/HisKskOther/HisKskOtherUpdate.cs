using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskOther
{
    partial class HisKskOtherUpdate : EntityBase
    {
        public HisKskOtherUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_OTHER>();
        }

        private BridgeDAO<HIS_KSK_OTHER> bridgeDAO;

        public bool Update(HIS_KSK_OTHER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_KSK_OTHER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
