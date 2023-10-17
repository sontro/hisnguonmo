using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisCareDetail
{
    partial class HisCareDetailCreate : EntityBase
    {
        public HisCareDetailCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE_DETAIL>();
        }

        private BridgeDAO<HIS_CARE_DETAIL> bridgeDAO;

        public bool Create(HIS_CARE_DETAIL data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CARE_DETAIL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
