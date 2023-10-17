using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceMachine
{
    partial class HisServiceMachineCreate : EntityBase
    {
        public HisServiceMachineCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_MACHINE>();
        }

        private BridgeDAO<HIS_SERVICE_MACHINE> bridgeDAO;

        public bool Create(HIS_SERVICE_MACHINE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_MACHINE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
