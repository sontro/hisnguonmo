using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExeServiceModule
{
    partial class HisExeServiceModuleCreate : EntityBase
    {
        public HisExeServiceModuleCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXE_SERVICE_MODULE>();
        }

        private BridgeDAO<HIS_EXE_SERVICE_MODULE> bridgeDAO;

        public bool Create(HIS_EXE_SERVICE_MODULE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXE_SERVICE_MODULE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
