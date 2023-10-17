using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ.Update
{
    class SereServPriceUtil
    {
        internal static void UpdateBedPrice(List<HIS_SERE_SERV> hisSereServs)
        {
            foreach (HIS_SERE_SERV d in hisSereServs)
            {
                if (d.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G && d.SHARE_COUNT.HasValue)
                {
                    long s = d.SHARE_COUNT.Value < 3 ? d.SHARE_COUNT.Value : 3;
                    if (BhytConstant.MAP_SHARED_BED_PRICE_RATIO.ContainsKey(s))
                    {
                        decimal ratio = BhytConstant.MAP_SHARED_BED_PRICE_RATIO[s];
                        SereServPriceUtil.SetPrice(d, d.ORIGINAL_PRICE * ratio);
                    }
                }
            }
        }

        internal static HIS_SERE_SERV SetPrice(HIS_SERE_SERV ss, decimal newPrice)
        {
            if (ss.HEIN_LIMIT_PRICE.HasValue)
            {
                ss.HEIN_LIMIT_PRICE = newPrice;
            }
            else
            {
                ss.PRICE = newPrice;
            }
            return ss;
        }

        /// <summary>
        /// Cap nhat lai thong tin gia
        /// </summary>
        /// <param name="treatment"></param>
        /// <param name="allSereServs"></param>
        /// <param name="effectedSereServIds"></param>
        internal static void ReloadPrice(CommonParam param, HIS_TREATMENT treatment, List<HIS_SERE_SERV> allSereServs, List<long> effectedSereServIds)
        {
            List<HIS_SERE_SERV> newInserts = null;
            SereServPriceUtil.ReloadPrice(param, treatment, allSereServs, newInserts, effectedSereServIds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="treatment"></param>
        /// <param name="allExistSereServs"></param>
        /// <param name="newInserts"></param>
        /// <param name="effectedSereServIds"></param>
        internal static void ReloadPrice(CommonParam param, HIS_TREATMENT treatment, List<HIS_SERE_SERV> allExistSereServs, List<HIS_SERE_SERV> newInserts, List<long> effectedSereServIds)
        {
            if (newInserts != null && newInserts.Count > 0)
            {
                long maxId = allExistSereServs != null && allExistSereServs.Count > 0 ? allExistSereServs.Max(o => o.ID) : 0;
                if (effectedSereServIds == null)
                {
                    effectedSereServIds = new List<long>();
                }

                foreach (HIS_SERE_SERV s in newInserts)
                {
                    s.ID = s.ID <= 0 ? ++maxId : s.ID;
                    effectedSereServIds.Add(s.ID);
                }
            }
            List<HIS_SERE_SERV> toProcess = new List<HIS_SERE_SERV>();
            if (allExistSereServs != null && allExistSereServs.Count > 0)
            {
                toProcess.AddRange(allExistSereServs);
            }
            if (newInserts != null && newInserts.Count > 0)
            {
                toProcess.AddRange(newInserts);
            }

            List<long> medicineIds = effectedSereServIds != null && toProcess != null ?
                toProcess.Where(o => o.MEDICINE_ID.HasValue && effectedSereServIds.Contains(o.ID))
                .Select(o => o.MEDICINE_ID.Value).ToList() : null;
            List<long> materialIds = effectedSereServIds != null && toProcess != null ?
                toProcess.Where(o => o.MATERIAL_ID.HasValue && effectedSereServIds.Contains(o.ID))
                .Select(o => o.MATERIAL_ID.Value).ToList() : null;

            if (effectedSereServIds != null && effectedSereServIds.Count > 0)
            {
                HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, medicineIds, materialIds);

                foreach (long id in effectedSereServIds)
                {
                    HIS_SERE_SERV t = toProcess.Where(o => o.ID == id).FirstOrDefault();
                    if (t != null && !priceAdder.AddPrice(t, toProcess, t.TDL_INTRUCTION_TIME, t.TDL_EXECUTE_BRANCH_ID, t.TDL_REQUEST_ROOM_ID, t.TDL_REQUEST_DEPARTMENT_ID, t.TDL_EXECUTE_ROOM_ID))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                }
            }
        }

        internal static void ReloadPrice(CommonParam param, HIS_TREATMENT treatment, List<HIS_SERE_SERV> allExistSereServs, HIS_SERE_SERV newInsert, List<long> effectedSereServIds)
        {
            List<HIS_SERE_SERV> newInserts = new List<HIS_SERE_SERV>();
            if (newInsert != null)
            {
                newInserts.Add(newInsert);
            }
            SereServPriceUtil.ReloadPrice(param, treatment, allExistSereServs, newInserts, effectedSereServIds);
        }

        internal static void SetPrimaryPatientTypeId(List<HIS_SERE_SERV> all, HIS_SERE_SERV s, HIS_PATIENT_TYPE_ALTER usingPta, HIS_TREATMENT treatment)
        {
            if (s.PATIENT_TYPE_ID != usingPta.PATIENT_TYPE_ID)
            {
                V_HIS_SERVICE_PATY primarySp = HisSereServPriceUtil.GetServicePaty(s, all, s.TDL_EXECUTE_BRANCH_ID, s.TDL_INTRUCTION_TIME, treatment.IN_TIME, s.PATIENT_TYPE_ID, s.SERVICE_ID, s.TDL_REQUEST_ROOM_ID, s.TDL_REQUEST_DEPARTMENT_ID, s.TDL_EXECUTE_ROOM_ID, s.PACKAGE_ID, s.SERVICE_CONDITION_ID, treatment.TDL_PATIENT_CLASSIFY_ID,s.TDL_RATION_TIME_ID);
                V_HIS_SERVICE_PATY sp = HisSereServPriceUtil.GetServicePaty(s, all, s.TDL_EXECUTE_BRANCH_ID, s.TDL_INTRUCTION_TIME, treatment.IN_TIME, usingPta.PATIENT_TYPE_ID, s.SERVICE_ID, s.TDL_REQUEST_ROOM_ID, s.TDL_REQUEST_DEPARTMENT_ID, s.TDL_EXECUTE_ROOM_ID, s.PACKAGE_ID, s.SERVICE_CONDITION_ID, treatment.TDL_PATIENT_CLASSIFY_ID,s.TDL_RATION_TIME_ID);

                //Neu gia tuong ung voi 2 doi tuong nay khac nhau hoac la dich vu tuong ung voi doi tuong BN la BHYT va co gia tran thi moi thuc hien gan lai
                bool isCheck = false;
                if (primarySp != null && sp != null)
                {
                    if (sp.PRICE < primarySp.PRICE)
                    {
                        isCheck = true;
                    }
                    else if (usingPta.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                    {
                        decimal? heinLimitRatio = null;
                        decimal? heinLimitPrice = null;
                        V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == s.SERVICE_ID).FirstOrDefault();
                        new HisSereServPriceUtil().GetHeinLimitPrice(service, s.TDL_INTRUCTION_TIME, treatment, s.SERVICE_CONDITION_ID, s.TDL_EXECUTE_BRANCH_ID, null, null, ref heinLimitPrice, ref heinLimitRatio);
                        isCheck = heinLimitPrice.HasValue || heinLimitRatio.HasValue;
                    }
                }

                if (isCheck)
                {
                    //primary_patient_type_id duoc gan bang doi tuong do nguoi dung chon
                    s.PRIMARY_PATIENT_TYPE_ID = s.PATIENT_TYPE_ID;
                    //patient_type_id duoc gan lai theo doi tuong BN
                    s.PATIENT_TYPE_ID = usingPta.PATIENT_TYPE_ID;
                }
            }
        }
    }
}
