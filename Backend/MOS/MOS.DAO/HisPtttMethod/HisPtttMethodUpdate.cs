using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttMethod
{
    partial class HisPtttMethodUpdate : EntityBase
    {
        public HisPtttMethodUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_METHOD>();
        }

        private BridgeDAO<HIS_PTTT_METHOD> bridgeDAO;

        public bool Update(HIS_PTTT_METHOD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PTTT_METHOD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
