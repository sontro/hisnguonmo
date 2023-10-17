using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisImpUserTempDt
{
    partial class HisImpUserTempDtCreate : EntityBase
    {
        public HisImpUserTempDtCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_USER_TEMP_DT>();
        }

        private BridgeDAO<HIS_IMP_USER_TEMP_DT> bridgeDAO;

        public bool Create(HIS_IMP_USER_TEMP_DT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_IMP_USER_TEMP_DT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
