using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisWelfareType
{
    partial class HisWelfareTypeCreate : EntityBase
    {
        public HisWelfareTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_WELFARE_TYPE>();
        }

        private BridgeDAO<HIS_WELFARE_TYPE> bridgeDAO;

        public bool Create(HIS_WELFARE_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_WELFARE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
