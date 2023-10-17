using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestStt
{
    partial class HisExpMestSttCreate : EntityBase
    {
        public HisExpMestSttCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_STT>();
        }

        private BridgeDAO<HIS_EXP_MEST_STT> bridgeDAO;

        public bool Create(HIS_EXP_MEST_STT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXP_MEST_STT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
