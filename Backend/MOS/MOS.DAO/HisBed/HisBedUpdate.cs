using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBed
{
    partial class HisBedUpdate : EntityBase
    {
        public HisBedUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED>();
        }

        private BridgeDAO<HIS_BED> bridgeDAO;

        public bool Update(HIS_BED data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BED> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
