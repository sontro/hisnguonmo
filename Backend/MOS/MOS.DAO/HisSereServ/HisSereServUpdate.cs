using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServ
{
    partial class HisSereServUpdate : EntityBase
    {
        public HisSereServUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV>();
        }

        private BridgeDAO<HIS_SERE_SERV> bridgeDAO;

        public bool Update(HIS_SERE_SERV data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERE_SERV> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
