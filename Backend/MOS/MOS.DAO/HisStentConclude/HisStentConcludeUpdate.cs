using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisStentConclude
{
    partial class HisStentConcludeUpdate : EntityBase
    {
        public HisStentConcludeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_STENT_CONCLUDE>();
        }

        private BridgeDAO<HIS_STENT_CONCLUDE> bridgeDAO;

        public bool Update(HIS_STENT_CONCLUDE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_STENT_CONCLUDE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
