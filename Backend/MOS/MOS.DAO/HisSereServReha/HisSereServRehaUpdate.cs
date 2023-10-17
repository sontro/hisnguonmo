using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServReha
{
    partial class HisSereServRehaUpdate : EntityBase
    {
        public HisSereServRehaUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_REHA>();
        }

        private BridgeDAO<HIS_SERE_SERV_REHA> bridgeDAO;

        public bool Update(HIS_SERE_SERV_REHA data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERE_SERV_REHA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
