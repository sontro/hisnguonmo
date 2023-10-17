using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServRation
{
    partial class HisSereServRationUpdate : EntityBase
    {
        public HisSereServRationUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_RATION>();
        }

        private BridgeDAO<HIS_SERE_SERV_RATION> bridgeDAO;

        public bool Update(HIS_SERE_SERV_RATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERE_SERV_RATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
