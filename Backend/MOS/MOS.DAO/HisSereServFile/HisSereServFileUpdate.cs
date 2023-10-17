using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServFile
{
    partial class HisSereServFileUpdate : EntityBase
    {
        public HisSereServFileUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_FILE>();
        }

        private BridgeDAO<HIS_SERE_SERV_FILE> bridgeDAO;

        public bool Update(HIS_SERE_SERV_FILE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERE_SERV_FILE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
