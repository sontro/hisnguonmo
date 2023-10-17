using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisKskOverEighteen
{
    partial class HisKskOverEighteenCreate : EntityBase
    {
        public HisKskOverEighteenCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_OVER_EIGHTEEN>();
        }

        private BridgeDAO<HIS_KSK_OVER_EIGHTEEN> bridgeDAO;

        public bool Create(HIS_KSK_OVER_EIGHTEEN data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_KSK_OVER_EIGHTEEN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
