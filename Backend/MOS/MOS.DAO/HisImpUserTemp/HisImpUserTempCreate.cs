using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisImpUserTemp
{
    partial class HisImpUserTempCreate : EntityBase
    {
        public HisImpUserTempCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_USER_TEMP>();
        }

        private BridgeDAO<HIS_IMP_USER_TEMP> bridgeDAO;

        public bool Create(HIS_IMP_USER_TEMP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_IMP_USER_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
