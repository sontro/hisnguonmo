using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHoreHoha
{
    partial class HisHoreHohaUpdate : EntityBase
    {
        public HisHoreHohaUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HORE_HOHA>();
        }

        private BridgeDAO<HIS_HORE_HOHA> bridgeDAO;

        public bool Update(HIS_HORE_HOHA data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_HORE_HOHA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
