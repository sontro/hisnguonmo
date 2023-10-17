using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSkinSurgeryDesc
{
    partial class HisSkinSurgeryDescUpdate : EntityBase
    {
        public HisSkinSurgeryDescUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SKIN_SURGERY_DESC>();
        }

        private BridgeDAO<HIS_SKIN_SURGERY_DESC> bridgeDAO;

        public bool Update(HIS_SKIN_SURGERY_DESC data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SKIN_SURGERY_DESC> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
