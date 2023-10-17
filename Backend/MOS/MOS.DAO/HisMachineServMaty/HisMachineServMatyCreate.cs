using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMachineServMaty
{
    partial class HisMachineServMatyCreate : EntityBase
    {
        public HisMachineServMatyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MACHINE_SERV_MATY>();
        }

        private BridgeDAO<HIS_MACHINE_SERV_MATY> bridgeDAO;

        public bool Create(HIS_MACHINE_SERV_MATY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MACHINE_SERV_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
