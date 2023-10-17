using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestStt
{
    partial class HisImpMestSttCreate : EntityBase
    {
        public HisImpMestSttCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_STT>();
        }

        private BridgeDAO<HIS_IMP_MEST_STT> bridgeDAO;

        public bool Create(HIS_IMP_MEST_STT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_IMP_MEST_STT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
