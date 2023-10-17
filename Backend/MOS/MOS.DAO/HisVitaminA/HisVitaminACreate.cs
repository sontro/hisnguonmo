using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisVitaminA
{
    partial class HisVitaminACreate : EntityBase
    {
        public HisVitaminACreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VITAMIN_A>();
        }

        private BridgeDAO<HIS_VITAMIN_A> bridgeDAO;

        public bool Create(HIS_VITAMIN_A data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_VITAMIN_A> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
