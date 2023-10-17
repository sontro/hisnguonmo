using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisFuexType
{
    partial class HisFuexTypeCreate : EntityBase
    {
        public HisFuexTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FUEX_TYPE>();
        }

        private BridgeDAO<HIS_FUEX_TYPE> bridgeDAO;

        public bool Create(HIS_FUEX_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_FUEX_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
