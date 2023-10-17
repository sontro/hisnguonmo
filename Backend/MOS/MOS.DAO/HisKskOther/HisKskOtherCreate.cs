using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisKskOther
{
    partial class HisKskOtherCreate : EntityBase
    {
        public HisKskOtherCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_OTHER>();
        }

        private BridgeDAO<HIS_KSK_OTHER> bridgeDAO;

        public bool Create(HIS_KSK_OTHER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_KSK_OTHER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
