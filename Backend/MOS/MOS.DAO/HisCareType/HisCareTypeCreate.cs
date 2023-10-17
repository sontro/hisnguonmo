using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisCareType
{
    partial class HisCareTypeCreate : EntityBase
    {
        public HisCareTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE_TYPE>();
        }

        private BridgeDAO<HIS_CARE_TYPE> bridgeDAO;

        public bool Create(HIS_CARE_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CARE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
