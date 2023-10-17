using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPatySub
{
    partial class HisMestPatySubUpdate : EntityBase
    {
        public HisMestPatySubUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PATY_SUB>();
        }

        private BridgeDAO<HIS_MEST_PATY_SUB> bridgeDAO;

        public bool Update(HIS_MEST_PATY_SUB data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEST_PATY_SUB> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
