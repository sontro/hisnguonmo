using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRationTime
{
    partial class HisRationTimeCreate : EntityBase
    {
        public HisRationTimeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_TIME>();
        }

        private BridgeDAO<HIS_RATION_TIME> bridgeDAO;

        public bool Create(HIS_RATION_TIME data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_RATION_TIME> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
