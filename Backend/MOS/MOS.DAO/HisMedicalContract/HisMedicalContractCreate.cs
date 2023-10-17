using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicalContract
{
    partial class HisMedicalContractCreate : EntityBase
    {
        public HisMedicalContractCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICAL_CONTRACT>();
        }

        private BridgeDAO<HIS_MEDICAL_CONTRACT> bridgeDAO;

        public bool Create(HIS_MEDICAL_CONTRACT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDICAL_CONTRACT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
