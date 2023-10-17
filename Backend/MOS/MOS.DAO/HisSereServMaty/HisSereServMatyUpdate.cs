using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServMaty
{
    partial class HisSereServMatyUpdate : EntityBase
    {
        public HisSereServMatyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_MATY>();
        }

        private BridgeDAO<HIS_SERE_SERV_MATY> bridgeDAO;

        public bool Update(HIS_SERE_SERV_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERE_SERV_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
