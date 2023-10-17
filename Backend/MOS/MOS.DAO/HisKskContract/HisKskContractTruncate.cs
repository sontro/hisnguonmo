using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskContract
{
    partial class HisKskContractTruncate : EntityBase
    {
        public HisKskContractTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_CONTRACT>();
        }

        private BridgeDAO<HIS_KSK_CONTRACT> bridgeDAO;

        public bool Truncate(HIS_KSK_CONTRACT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_KSK_CONTRACT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
