using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTranPatiTemp
{
    partial class HisTranPatiTempUpdate : EntityBase
    {
        public HisTranPatiTempUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRAN_PATI_TEMP>();
        }

        private BridgeDAO<HIS_TRAN_PATI_TEMP> bridgeDAO;

        public bool Update(HIS_TRAN_PATI_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TRAN_PATI_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
