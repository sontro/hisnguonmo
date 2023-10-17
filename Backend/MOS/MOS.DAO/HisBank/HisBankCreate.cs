using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBank
{
    partial class HisBankCreate : EntityBase
    {
        public HisBankCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BANK>();
        }

        private BridgeDAO<HIS_BANK> bridgeDAO;

        public bool Create(HIS_BANK data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BANK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
