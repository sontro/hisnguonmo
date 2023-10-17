using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisWorkingShift
{
    partial class HisWorkingShiftCreate : EntityBase
    {
        public HisWorkingShiftCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_WORKING_SHIFT>();
        }

        private BridgeDAO<HIS_WORKING_SHIFT> bridgeDAO;

        public bool Create(HIS_WORKING_SHIFT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_WORKING_SHIFT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
