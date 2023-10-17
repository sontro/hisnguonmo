using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebate
{
    partial class HisDebateGet : BusinessBase
    {
        public List<HIS_DEBATE> GetByMedicine(string cardCode)
        {
            //List<HisPatientSDO> result = new List<HisPatientSDO>();
            //string query = string.Format("SELECT * FROM D_HIS_PATIENT_CARD WHERE CARD_CODE = '{0}' ORDER BY LOG_TIME DESC ", cardCode);

            //List<D_HIS_PATIENT_CARD> data = DAOWorker.SqlDAO.GetSql<D_HIS_PATIENT_CARD>(query);
            //if (IsNotNullOrEmpty(data))
            //{
            //    D_HIS_PATIENT_CARD tmp = data[0];
            //    Mapper.CreateMap<D_HIS_PATIENT_CARD, HisPatientSDO>();
            //    HisPatientSDO sdo = Mapper.Map<HisPatientSDO>(tmp);
            //    sdo.HeinCardNumber = tmp.HEIN_CARD_NUMBER;
            //    sdo.HeinAddress = tmp.BHYT_ADDRESS;
            //    sdo.HeinCardFromTime = tmp.HEIN_CARD_FROM_TIME;
            //    sdo.HeinCardToTime = tmp.HEIN_CARD_TO_TIME;
            //    sdo.HeinMediOrgCode = tmp.HEIN_MEDI_ORG_CODE;
            //    sdo.HeinMediOrgName = tmp.HEIN_MEDI_ORG_NAME;
            //    sdo.Join5Year = tmp.JOIN_5_YEAR;
            //    sdo.Paid6Month = tmp.PAID_6_MONTH;
            //    sdo.RightRouteCode = tmp.RIGHT_ROUTE_CODE;
            //    sdo.RightRouteTypeCode = tmp.RIGHT_ROUTE_TYPE_CODE;
            //    sdo.LiveAreaCode = tmp.LIVE_AREA_CODE;
            //    sdo.CardCode = tmp.CARD_CODE;
            //    sdo.CardId = tmp.CARD_ID;
            //    result.Add(sdo);
            //}
            //return result;
            return null;
        }
    }
}
