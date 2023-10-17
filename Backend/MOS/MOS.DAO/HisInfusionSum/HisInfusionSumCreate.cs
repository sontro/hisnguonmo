using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisInfusionSum
{
    partial class HisInfusionSumCreate : EntityBase
    {
        public HisInfusionSumCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INFUSION_SUM>();
        }

        private BridgeDAO<HIS_INFUSION_SUM> bridgeDAO;

        public bool Create(HIS_INFUSION_SUM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_INFUSION_SUM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
