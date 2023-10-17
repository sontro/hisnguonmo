using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBloodType
{
    partial class HisBloodTypeCreate : EntityBase
    {
        public HisBloodTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_TYPE>();
        }

        private BridgeDAO<HIS_BLOOD_TYPE> bridgeDAO;

        public bool Create(HIS_BLOOD_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BLOOD_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
