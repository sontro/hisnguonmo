using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediReact
{
    partial class HisMediReactTruncate : EntityBase
    {
        public HisMediReactTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_REACT>();
        }

        private BridgeDAO<HIS_MEDI_REACT> bridgeDAO;

        public bool Truncate(HIS_MEDI_REACT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDI_REACT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
