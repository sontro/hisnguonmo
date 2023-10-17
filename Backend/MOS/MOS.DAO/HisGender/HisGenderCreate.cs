using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisGender
{
    partial class HisGenderCreate : EntityBase
    {
        public HisGenderCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_GENDER>();
        }

        private BridgeDAO<HIS_GENDER> bridgeDAO;

        public bool Create(HIS_GENDER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_GENDER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
