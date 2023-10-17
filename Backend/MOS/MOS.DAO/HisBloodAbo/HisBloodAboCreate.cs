using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBloodAbo
{
    partial class HisBloodAboCreate : EntityBase
    {
        public HisBloodAboCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_ABO>();
        }

        private BridgeDAO<HIS_BLOOD_ABO> bridgeDAO;

        public bool Create(HIS_BLOOD_ABO data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BLOOD_ABO> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
