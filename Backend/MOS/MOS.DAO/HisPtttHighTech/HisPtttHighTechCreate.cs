using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttHighTech
{
    partial class HisPtttHighTechCreate : EntityBase
    {
        public HisPtttHighTechCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_HIGH_TECH>();
        }

        private BridgeDAO<HIS_PTTT_HIGH_TECH> bridgeDAO;

        public bool Create(HIS_PTTT_HIGH_TECH data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PTTT_HIGH_TECH> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
