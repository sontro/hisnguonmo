using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBloodGroup
{
    partial class HisBloodGroupCreate : EntityBase
    {
        public HisBloodGroupCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_GROUP>();
        }

        private BridgeDAO<HIS_BLOOD_GROUP> bridgeDAO;

        public bool Create(HIS_BLOOD_GROUP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BLOOD_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
