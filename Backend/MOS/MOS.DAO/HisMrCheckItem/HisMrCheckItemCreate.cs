using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMrCheckItem
{
    partial class HisMrCheckItemCreate : EntityBase
    {
        public HisMrCheckItemCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MR_CHECK_ITEM>();
        }

        private BridgeDAO<HIS_MR_CHECK_ITEM> bridgeDAO;

        public bool Create(HIS_MR_CHECK_ITEM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MR_CHECK_ITEM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
