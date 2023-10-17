using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientClassify
{
    partial class HisPatientClassifyCreate : EntityBase
    {
        public HisPatientClassifyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_CLASSIFY>();
        }

        private BridgeDAO<HIS_PATIENT_CLASSIFY> bridgeDAO;

        public bool Create(HIS_PATIENT_CLASSIFY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PATIENT_CLASSIFY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
