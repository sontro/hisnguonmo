using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisProcessingMethod
{
    partial class HisProcessingMethodCreate : EntityBase
    {
        public HisProcessingMethodCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PROCESSING_METHOD>();
        }

        private BridgeDAO<HIS_PROCESSING_METHOD> bridgeDAO;

        public bool Create(HIS_PROCESSING_METHOD data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PROCESSING_METHOD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
