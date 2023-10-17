using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTranPatiTech
{
    partial class HisTranPatiTechUpdate : EntityBase
    {
        public HisTranPatiTechUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRAN_PATI_TECH>();
        }

        private BridgeDAO<HIS_TRAN_PATI_TECH> bridgeDAO;

        public bool Update(HIS_TRAN_PATI_TECH data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TRAN_PATI_TECH> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
