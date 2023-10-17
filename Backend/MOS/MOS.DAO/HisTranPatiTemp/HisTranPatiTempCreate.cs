using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTranPatiTemp
{
    partial class HisTranPatiTempCreate : EntityBase
    {
        public HisTranPatiTempCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRAN_PATI_TEMP>();
        }

        private BridgeDAO<HIS_TRAN_PATI_TEMP> bridgeDAO;

        public bool Create(HIS_TRAN_PATI_TEMP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TRAN_PATI_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
