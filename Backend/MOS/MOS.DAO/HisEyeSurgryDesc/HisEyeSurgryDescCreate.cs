using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEyeSurgryDesc
{
    partial class HisEyeSurgryDescCreate : EntityBase
    {
        public HisEyeSurgryDescCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EYE_SURGRY_DESC>();
        }

        private BridgeDAO<HIS_EYE_SURGRY_DESC> bridgeDAO;

        public bool Create(HIS_EYE_SURGRY_DESC data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EYE_SURGRY_DESC> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
