using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentEndTypeExt
{
    partial class HisTreatmentEndTypeExtCreate : EntityBase
    {
        public HisTreatmentEndTypeExtCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_END_TYPE_EXT>();
        }

        private BridgeDAO<HIS_TREATMENT_END_TYPE_EXT> bridgeDAO;

        public bool Create(HIS_TREATMENT_END_TYPE_EXT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TREATMENT_END_TYPE_EXT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
