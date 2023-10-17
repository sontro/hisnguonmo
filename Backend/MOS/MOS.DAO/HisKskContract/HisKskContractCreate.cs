using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisKskContract
{
    partial class HisKskContractCreate : EntityBase
    {
        public HisKskContractCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_CONTRACT>();
        }

        private BridgeDAO<HIS_KSK_CONTRACT> bridgeDAO;

        public bool Create(HIS_KSK_CONTRACT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_KSK_CONTRACT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
