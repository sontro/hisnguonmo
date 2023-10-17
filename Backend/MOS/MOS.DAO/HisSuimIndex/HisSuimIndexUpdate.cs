using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSuimIndex
{
    partial class HisSuimIndexUpdate : EntityBase
    {
        public HisSuimIndexUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUIM_INDEX>();
        }

        private BridgeDAO<HIS_SUIM_INDEX> bridgeDAO;

        public bool Update(HIS_SUIM_INDEX data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SUIM_INDEX> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
