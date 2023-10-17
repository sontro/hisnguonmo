using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisCareSum
{
    partial class HisCareSumCreate : EntityBase
    {
        public HisCareSumCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE_SUM>();
        }

        private BridgeDAO<HIS_CARE_SUM> bridgeDAO;

        public bool Create(HIS_CARE_SUM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CARE_SUM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
