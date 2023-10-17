using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDocHoldType
{
    partial class HisDocHoldTypeCreate : EntityBase
    {
        public HisDocHoldTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DOC_HOLD_TYPE>();
        }

        private BridgeDAO<HIS_DOC_HOLD_TYPE> bridgeDAO;

        public bool Create(HIS_DOC_HOLD_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DOC_HOLD_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
