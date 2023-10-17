using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccReactType
{
    partial class HisVaccReactTypeTruncate : EntityBase
    {
        public HisVaccReactTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_REACT_TYPE>();
        }

        private BridgeDAO<HIS_VACC_REACT_TYPE> bridgeDAO;

        public bool Truncate(HIS_VACC_REACT_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_VACC_REACT_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
