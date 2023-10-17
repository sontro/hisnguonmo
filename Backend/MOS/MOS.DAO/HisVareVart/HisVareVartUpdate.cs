using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVareVart
{
    partial class HisVareVartUpdate : EntityBase
    {
        public HisVareVartUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VARE_VART>();
        }

        private BridgeDAO<HIS_VARE_VART> bridgeDAO;

        public bool Update(HIS_VARE_VART data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_VARE_VART> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
