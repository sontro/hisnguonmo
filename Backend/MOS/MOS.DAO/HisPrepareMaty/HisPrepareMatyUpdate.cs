using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPrepareMaty
{
    partial class HisPrepareMatyUpdate : EntityBase
    {
        public HisPrepareMatyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PREPARE_MATY>();
        }

        private BridgeDAO<HIS_PREPARE_MATY> bridgeDAO;

        public bool Update(HIS_PREPARE_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PREPARE_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
