using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisObeyContraindi
{
    partial class HisObeyContraindiTruncate : EntityBase
    {
        public HisObeyContraindiTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_OBEY_CONTRAINDI>();
        }

        private BridgeDAO<HIS_OBEY_CONTRAINDI> bridgeDAO;

        public bool Truncate(HIS_OBEY_CONTRAINDI data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_OBEY_CONTRAINDI> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
