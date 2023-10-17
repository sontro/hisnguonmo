using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisKskUnderEighteen
{
    partial class HisKskUnderEighteenCreate : EntityBase
    {
        public HisKskUnderEighteenCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_UNDER_EIGHTEEN>();
        }

        private BridgeDAO<HIS_KSK_UNDER_EIGHTEEN> bridgeDAO;

        public bool Create(HIS_KSK_UNDER_EIGHTEEN data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_KSK_UNDER_EIGHTEEN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
