using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttCatastrophe
{
    partial class HisPtttCatastropheUpdate : EntityBase
    {
        public HisPtttCatastropheUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_CATASTROPHE>();
        }

        private BridgeDAO<HIS_PTTT_CATASTROPHE> bridgeDAO;

        public bool Update(HIS_PTTT_CATASTROPHE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PTTT_CATASTROPHE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
