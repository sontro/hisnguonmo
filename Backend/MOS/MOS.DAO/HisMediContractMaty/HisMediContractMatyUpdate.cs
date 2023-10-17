using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediContractMaty
{
    partial class HisMediContractMatyUpdate : EntityBase
    {
        public HisMediContractMatyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_CONTRACT_MATY>();
        }

        private BridgeDAO<HIS_MEDI_CONTRACT_MATY> bridgeDAO;

        public bool Update(HIS_MEDI_CONTRACT_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDI_CONTRACT_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
