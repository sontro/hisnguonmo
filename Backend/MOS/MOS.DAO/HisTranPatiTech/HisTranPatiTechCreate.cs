using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTranPatiTech
{
    partial class HisTranPatiTechCreate : EntityBase
    {
        public HisTranPatiTechCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRAN_PATI_TECH>();
        }

        private BridgeDAO<HIS_TRAN_PATI_TECH> bridgeDAO;

        public bool Create(HIS_TRAN_PATI_TECH data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TRAN_PATI_TECH> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
