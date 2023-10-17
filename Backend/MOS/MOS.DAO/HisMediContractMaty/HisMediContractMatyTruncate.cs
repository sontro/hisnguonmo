using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediContractMaty
{
    partial class HisMediContractMatyTruncate : EntityBase
    {
        public HisMediContractMatyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_CONTRACT_MATY>();
        }

        private BridgeDAO<HIS_MEDI_CONTRACT_MATY> bridgeDAO;

        public bool Truncate(HIS_MEDI_CONTRACT_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDI_CONTRACT_MATY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
