using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisKskDriver
{
    partial class HisKskDriverCreate : EntityBase
    {
        public HisKskDriverCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_DRIVER>();
        }

        private BridgeDAO<HIS_KSK_DRIVER> bridgeDAO;

        public bool Create(HIS_KSK_DRIVER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_KSK_DRIVER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
