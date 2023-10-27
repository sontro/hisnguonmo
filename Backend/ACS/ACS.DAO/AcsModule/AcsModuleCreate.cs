using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsModule
{
    partial class AcsModuleCreate : EntityBase
    {
        public AcsModuleCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_MODULE>();
        }

        private BridgeDAO<ACS_MODULE> bridgeDAO;

        public bool Create(ACS_MODULE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_MODULE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
