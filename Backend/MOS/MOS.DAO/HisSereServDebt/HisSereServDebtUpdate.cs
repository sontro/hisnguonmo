using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServDebt
{
    partial class HisSereServDebtUpdate : EntityBase
    {
        public HisSereServDebtUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_DEBT>();
        }

        private BridgeDAO<HIS_SERE_SERV_DEBT> bridgeDAO;

        public bool Update(HIS_SERE_SERV_DEBT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERE_SERV_DEBT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
