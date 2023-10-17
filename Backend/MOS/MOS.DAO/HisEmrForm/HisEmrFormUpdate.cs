using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmrForm
{
    partial class HisEmrFormUpdate : EntityBase
    {
        public HisEmrFormUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMR_FORM>();
        }

        private BridgeDAO<HIS_EMR_FORM> bridgeDAO;

        public bool Update(HIS_EMR_FORM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EMR_FORM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
