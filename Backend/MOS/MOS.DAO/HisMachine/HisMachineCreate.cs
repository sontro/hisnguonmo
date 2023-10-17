using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMachine
{
    partial class HisMachineCreate : EntityBase
    {
        public HisMachineCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MACHINE>();
        }

        private BridgeDAO<HIS_MACHINE> bridgeDAO;

        public bool Create(HIS_MACHINE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MACHINE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
