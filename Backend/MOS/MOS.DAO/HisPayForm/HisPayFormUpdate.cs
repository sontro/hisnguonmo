using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPayForm
{
    partial class HisPayFormUpdate : EntityBase
    {
        public HisPayFormUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PAY_FORM>();
        }

        private BridgeDAO<HIS_PAY_FORM> bridgeDAO;

        public bool Update(HIS_PAY_FORM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PAY_FORM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
