using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisKskService
{
    partial class HisKskServiceCreate : EntityBase
    {
        public HisKskServiceCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_SERVICE>();
        }

        private BridgeDAO<HIS_KSK_SERVICE> bridgeDAO;

        public bool Create(HIS_KSK_SERVICE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_KSK_SERVICE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
