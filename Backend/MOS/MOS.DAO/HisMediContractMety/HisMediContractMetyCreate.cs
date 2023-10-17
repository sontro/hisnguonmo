using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMediContractMety
{
    partial class HisMediContractMetyCreate : EntityBase
    {
        public HisMediContractMetyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_CONTRACT_METY>();
        }

        private BridgeDAO<HIS_MEDI_CONTRACT_METY> bridgeDAO;

        public bool Create(HIS_MEDI_CONTRACT_METY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDI_CONTRACT_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
