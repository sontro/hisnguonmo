using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServDeposit
{
    partial class HisSereServDepositUpdate : EntityBase
    {
        public HisSereServDepositUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_DEPOSIT>();
        }

        private BridgeDAO<HIS_SERE_SERV_DEPOSIT> bridgeDAO;

        public bool Update(HIS_SERE_SERV_DEPOSIT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERE_SERV_DEPOSIT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
