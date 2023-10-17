using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSesePtttMethod
{
    partial class HisSesePtttMethodCreate : EntityBase
    {
        public HisSesePtttMethodCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SESE_PTTT_METHOD>();
        }

        private BridgeDAO<HIS_SESE_PTTT_METHOD> bridgeDAO;

        public bool Create(HIS_SESE_PTTT_METHOD data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SESE_PTTT_METHOD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
