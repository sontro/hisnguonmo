using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPatientType
{
    partial class HisMestPatientTypeCreate : EntityBase
    {
        public HisMestPatientTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PATIENT_TYPE>();
        }

        private BridgeDAO<HIS_MEST_PATIENT_TYPE> bridgeDAO;

        public bool Create(HIS_MEST_PATIENT_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEST_PATIENT_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
