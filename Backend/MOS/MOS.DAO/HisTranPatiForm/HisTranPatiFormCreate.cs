using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTranPatiForm
{
    partial class HisTranPatiFormCreate : EntityBase
    {
        public HisTranPatiFormCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRAN_PATI_FORM>();
        }

        private BridgeDAO<HIS_TRAN_PATI_FORM> bridgeDAO;

        public bool Create(HIS_TRAN_PATI_FORM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TRAN_PATI_FORM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
