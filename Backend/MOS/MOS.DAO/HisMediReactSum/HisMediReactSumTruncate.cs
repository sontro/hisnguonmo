using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediReactSum
{
    partial class HisMediReactSumTruncate : EntityBase
    {
        public HisMediReactSumTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_REACT_SUM>();
        }

        private BridgeDAO<HIS_MEDI_REACT_SUM> bridgeDAO;

        public bool Truncate(HIS_MEDI_REACT_SUM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDI_REACT_SUM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
