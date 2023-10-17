using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediContractMety
{
    partial class HisMediContractMetyUpdate : EntityBase
    {
        public HisMediContractMetyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_CONTRACT_METY>();
        }

        private BridgeDAO<HIS_MEDI_CONTRACT_METY> bridgeDAO;

        public bool Update(HIS_MEDI_CONTRACT_METY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDI_CONTRACT_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
