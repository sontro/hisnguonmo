using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServPttt
{
    partial class HisSereServPtttUpdate : EntityBase
    {
        public HisSereServPtttUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_PTTT>();
        }

        private BridgeDAO<HIS_SERE_SERV_PTTT> bridgeDAO;

        public bool Update(HIS_SERE_SERV_PTTT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERE_SERV_PTTT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
