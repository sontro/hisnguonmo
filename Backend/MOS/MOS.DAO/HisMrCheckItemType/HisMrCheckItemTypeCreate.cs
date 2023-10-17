using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMrCheckItemType
{
    partial class HisMrCheckItemTypeCreate : EntityBase
    {
        public HisMrCheckItemTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MR_CHECK_ITEM_TYPE>();
        }

        private BridgeDAO<HIS_MR_CHECK_ITEM_TYPE> bridgeDAO;

        public bool Create(HIS_MR_CHECK_ITEM_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MR_CHECK_ITEM_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
