using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServSuin
{
    partial class HisSereServSuinUpdate : EntityBase
    {
        public HisSereServSuinUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_SUIN>();
        }

        private BridgeDAO<HIS_SERE_SERV_SUIN> bridgeDAO;

        public bool Update(HIS_SERE_SERV_SUIN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERE_SERV_SUIN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
