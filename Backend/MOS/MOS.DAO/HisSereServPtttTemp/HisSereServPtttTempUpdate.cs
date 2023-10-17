using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServPtttTemp
{
    partial class HisSereServPtttTempUpdate : EntityBase
    {
        public HisSereServPtttTempUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_PTTT_TEMP>();
        }

        private BridgeDAO<HIS_SERE_SERV_PTTT_TEMP> bridgeDAO;

        public bool Update(HIS_SERE_SERV_PTTT_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERE_SERV_PTTT_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
