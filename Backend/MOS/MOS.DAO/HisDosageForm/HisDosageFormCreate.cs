using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDosageForm
{
    partial class HisDosageFormCreate : EntityBase
    {
        public HisDosageFormCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DOSAGE_FORM>();
        }

        private BridgeDAO<HIS_DOSAGE_FORM> bridgeDAO;

        public bool Create(HIS_DOSAGE_FORM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DOSAGE_FORM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
