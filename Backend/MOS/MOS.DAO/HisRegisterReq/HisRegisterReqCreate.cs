using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRegisterReq
{
    partial class HisRegisterReqCreate : EntityBase
    {
        public HisRegisterReqCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REGISTER_REQ>();
        }

        private BridgeDAO<HIS_REGISTER_REQ> bridgeDAO;

        public bool Create(HIS_REGISTER_REQ data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_REGISTER_REQ> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
