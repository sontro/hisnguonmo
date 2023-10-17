using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisWelfareType
{
    partial class HisWelfareTypeUpdate : EntityBase
    {
        public HisWelfareTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_WELFARE_TYPE>();
        }

        private BridgeDAO<HIS_WELFARE_TYPE> bridgeDAO;

        public bool Update(HIS_WELFARE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_WELFARE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
