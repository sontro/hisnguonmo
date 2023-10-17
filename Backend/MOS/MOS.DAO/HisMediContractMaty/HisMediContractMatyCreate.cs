using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMediContractMaty
{
    partial class HisMediContractMatyCreate : EntityBase
    {
        public HisMediContractMatyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_CONTRACT_MATY>();
        }

        private BridgeDAO<HIS_MEDI_CONTRACT_MATY> bridgeDAO;

        public bool Create(HIS_MEDI_CONTRACT_MATY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDI_CONTRACT_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
