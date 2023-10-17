using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisInfusion
{
    partial class HisInfusionCreate : EntityBase
    {
        public HisInfusionCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INFUSION>();
        }

        private BridgeDAO<HIS_INFUSION> bridgeDAO;

        public bool Create(HIS_INFUSION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_INFUSION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
