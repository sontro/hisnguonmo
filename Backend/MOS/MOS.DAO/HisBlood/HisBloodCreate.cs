using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBlood
{
    partial class HisBloodCreate : EntityBase
    {
        public HisBloodCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD>();
        }

        private BridgeDAO<HIS_BLOOD> bridgeDAO;

        public bool Create(HIS_BLOOD data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BLOOD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
