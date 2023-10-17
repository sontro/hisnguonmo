using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSuimSetySuin
{
    partial class HisSuimSetySuinUpdate : EntityBase
    {
        public HisSuimSetySuinUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUIM_SETY_SUIN>();
        }

        private BridgeDAO<HIS_SUIM_SETY_SUIN> bridgeDAO;

        public bool Update(HIS_SUIM_SETY_SUIN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SUIM_SETY_SUIN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
