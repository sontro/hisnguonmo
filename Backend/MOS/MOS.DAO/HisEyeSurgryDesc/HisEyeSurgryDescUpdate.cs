using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEyeSurgryDesc
{
    partial class HisEyeSurgryDescUpdate : EntityBase
    {
        public HisEyeSurgryDescUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EYE_SURGRY_DESC>();
        }

        private BridgeDAO<HIS_EYE_SURGRY_DESC> bridgeDAO;

        public bool Update(HIS_EYE_SURGRY_DESC data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EYE_SURGRY_DESC> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
