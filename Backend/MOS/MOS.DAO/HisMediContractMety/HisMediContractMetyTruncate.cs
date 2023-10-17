using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediContractMety
{
    partial class HisMediContractMetyTruncate : EntityBase
    {
        public HisMediContractMetyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_CONTRACT_METY>();
        }

        private BridgeDAO<HIS_MEDI_CONTRACT_METY> bridgeDAO;

        public bool Truncate(HIS_MEDI_CONTRACT_METY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDI_CONTRACT_METY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
