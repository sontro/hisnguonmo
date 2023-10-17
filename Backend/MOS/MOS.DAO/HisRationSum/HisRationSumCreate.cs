using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRationSum
{
    partial class HisRationSumCreate : EntityBase
    {
        public HisRationSumCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_SUM>();
        }

        private BridgeDAO<HIS_RATION_SUM> bridgeDAO;

        public bool Create(HIS_RATION_SUM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_RATION_SUM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
