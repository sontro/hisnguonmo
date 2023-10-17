using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSpeedUnit
{
    partial class HisSpeedUnitCreate : EntityBase
    {
        public HisSpeedUnitCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SPEED_UNIT>();
        }

        private BridgeDAO<HIS_SPEED_UNIT> bridgeDAO;

        public bool Create(HIS_SPEED_UNIT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SPEED_UNIT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
