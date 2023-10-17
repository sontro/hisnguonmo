using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTranPatiForm
{
    partial class HisTranPatiFormUpdate : EntityBase
    {
        public HisTranPatiFormUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRAN_PATI_FORM>();
        }

        private BridgeDAO<HIS_TRAN_PATI_FORM> bridgeDAO;

        public bool Update(HIS_TRAN_PATI_FORM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TRAN_PATI_FORM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
