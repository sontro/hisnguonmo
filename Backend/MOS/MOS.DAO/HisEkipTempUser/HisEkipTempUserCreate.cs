using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEkipTempUser
{
    partial class HisEkipTempUserCreate : EntityBase
    {
        public HisEkipTempUserCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EKIP_TEMP_USER>();
        }

        private BridgeDAO<HIS_EKIP_TEMP_USER> bridgeDAO;

        public bool Create(HIS_EKIP_TEMP_USER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EKIP_TEMP_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
