using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServTemp
{
    partial class HisSereServTempUpdate : EntityBase
    {
        public HisSereServTempUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_TEMP>();
        }

        private BridgeDAO<HIS_SERE_SERV_TEMP> bridgeDAO;

        public bool Update(HIS_SERE_SERV_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERE_SERV_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
