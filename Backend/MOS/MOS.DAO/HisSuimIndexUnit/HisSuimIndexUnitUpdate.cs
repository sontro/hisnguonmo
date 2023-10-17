using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSuimIndexUnit
{
    partial class HisSuimIndexUnitUpdate : EntityBase
    {
        public HisSuimIndexUnitUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUIM_INDEX_UNIT>();
        }

        private BridgeDAO<HIS_SUIM_INDEX_UNIT> bridgeDAO;

        public bool Update(HIS_SUIM_INDEX_UNIT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SUIM_INDEX_UNIT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
