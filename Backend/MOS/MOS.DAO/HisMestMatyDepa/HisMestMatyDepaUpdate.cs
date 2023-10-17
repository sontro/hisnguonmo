using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestMatyDepa
{
    partial class HisMestMatyDepaUpdate : EntityBase
    {
        public HisMestMatyDepaUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_MATY_DEPA>();
        }

        private BridgeDAO<HIS_MEST_MATY_DEPA> bridgeDAO;

        public bool Update(HIS_MEST_MATY_DEPA data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEST_MATY_DEPA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
