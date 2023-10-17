using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBloodRh
{
    partial class HisBloodRhCreate : EntityBase
    {
        public HisBloodRhCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_RH>();
        }

        private BridgeDAO<HIS_BLOOD_RH> bridgeDAO;

        public bool Create(HIS_BLOOD_RH data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BLOOD_RH> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
