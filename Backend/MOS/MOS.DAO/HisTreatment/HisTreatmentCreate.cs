using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatment
{
    partial class HisTreatmentCreate : EntityBase
    {
        public HisTreatmentCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT>();
        }

        private BridgeDAO<HIS_TREATMENT> bridgeDAO;

        public bool Create(HIS_TREATMENT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TREATMENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
