using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBaby
{
    partial class HisBabyCreate : EntityBase
    {
        public HisBabyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BABY>();
        }

        private BridgeDAO<HIS_BABY> bridgeDAO;

        public bool Create(HIS_BABY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BABY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
