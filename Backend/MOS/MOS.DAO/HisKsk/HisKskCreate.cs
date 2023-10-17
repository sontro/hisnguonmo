using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisKsk
{
    partial class HisKskCreate : EntityBase
    {
        public HisKskCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK>();
        }

        private BridgeDAO<HIS_KSK> bridgeDAO;

        public bool Create(HIS_KSK data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_KSK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
