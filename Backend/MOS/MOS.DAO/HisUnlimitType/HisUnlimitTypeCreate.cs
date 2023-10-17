using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisUnlimitType
{
    partial class HisUnlimitTypeCreate : EntityBase
    {
        public HisUnlimitTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_UNLIMIT_TYPE>();
        }

        private BridgeDAO<HIS_UNLIMIT_TYPE> bridgeDAO;

        public bool Create(HIS_UNLIMIT_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_UNLIMIT_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
