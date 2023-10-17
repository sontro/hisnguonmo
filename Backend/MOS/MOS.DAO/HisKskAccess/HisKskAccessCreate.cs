using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisKskAccess
{
    partial class HisKskAccessCreate : EntityBase
    {
        public HisKskAccessCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_ACCESS>();
        }

        private BridgeDAO<HIS_KSK_ACCESS> bridgeDAO;

        public bool Create(HIS_KSK_ACCESS data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_KSK_ACCESS> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
