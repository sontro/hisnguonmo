using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccReactPlace
{
    partial class HisVaccReactPlaceTruncate : EntityBase
    {
        public HisVaccReactPlaceTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_REACT_PLACE>();
        }

        private BridgeDAO<HIS_VACC_REACT_PLACE> bridgeDAO;

        public bool Truncate(HIS_VACC_REACT_PLACE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_VACC_REACT_PLACE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
