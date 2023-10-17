using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSpeciality
{
    partial class HisSpecialityCreate : EntityBase
    {
        public HisSpecialityCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SPECIALITY>();
        }

        private BridgeDAO<HIS_SPECIALITY> bridgeDAO;

        public bool Create(HIS_SPECIALITY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SPECIALITY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
