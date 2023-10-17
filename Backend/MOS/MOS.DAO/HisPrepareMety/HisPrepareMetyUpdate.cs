using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPrepareMety
{
    partial class HisPrepareMetyUpdate : EntityBase
    {
        public HisPrepareMetyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PREPARE_METY>();
        }

        private BridgeDAO<HIS_PREPARE_METY> bridgeDAO;

        public bool Update(HIS_PREPARE_METY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PREPARE_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
