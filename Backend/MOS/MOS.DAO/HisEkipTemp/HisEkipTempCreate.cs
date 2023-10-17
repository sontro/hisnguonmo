using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEkipTemp
{
    partial class HisEkipTempCreate : EntityBase
    {
        public HisEkipTempCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EKIP_TEMP>();
        }

        private BridgeDAO<HIS_EKIP_TEMP> bridgeDAO;

        public bool Create(HIS_EKIP_TEMP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EKIP_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
