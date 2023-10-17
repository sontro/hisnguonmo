using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRationSumStt
{
    partial class HisRationSumSttCreate : EntityBase
    {
        public HisRationSumSttCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_SUM_STT>();
        }

        private BridgeDAO<HIS_RATION_SUM_STT> bridgeDAO;

        public bool Create(HIS_RATION_SUM_STT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_RATION_SUM_STT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
