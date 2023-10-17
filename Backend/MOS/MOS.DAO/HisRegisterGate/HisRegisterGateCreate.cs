using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRegisterGate
{
    partial class HisRegisterGateCreate : EntityBase
    {
        public HisRegisterGateCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REGISTER_GATE>();
        }

        private BridgeDAO<HIS_REGISTER_GATE> bridgeDAO;

        public bool Create(HIS_REGISTER_GATE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_REGISTER_GATE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
