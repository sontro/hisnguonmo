using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisCareTemp
{
    partial class HisCareTempCreate : EntityBase
    {
        public HisCareTempCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE_TEMP>();
        }

        private BridgeDAO<HIS_CARE_TEMP> bridgeDAO;

        public bool Create(HIS_CARE_TEMP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CARE_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
