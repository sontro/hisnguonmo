using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestMetyDepa
{
    partial class HisMestMetyDepaUpdate : EntityBase
    {
        public HisMestMetyDepaUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_METY_DEPA>();
        }

        private BridgeDAO<HIS_MEST_METY_DEPA> bridgeDAO;

        public bool Update(HIS_MEST_METY_DEPA data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEST_METY_DEPA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
