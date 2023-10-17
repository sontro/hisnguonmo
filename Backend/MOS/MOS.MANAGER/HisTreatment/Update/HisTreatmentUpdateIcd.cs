using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisServiceReq;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.Config;
using MOS.UTILITY;

namespace MOS.MANAGER.HisTreatment
{
    class IcdInfo
    {
        public long SERVICE_REQ_ID {get;set;}
        public string ICD_CODE { get; set; }
        public string ICD_NAME { get; set; }
        public string ICD_SUB_CODE { get; set; }
        public string ICD_TEXT { get; set; }
    }

    partial class HisTreatmentUpdate : BusinessBase
    {
        /// <summary>
        /// Bo sung thong tin ICD vao treatment (nhung ko update DB)
        /// </summary>
        /// <param name="newServiceReq"></param>
        /// <param name="oldServiceReq"></param>
        /// <param name="treatment"></param>
        /// <returns></returns>
        internal static bool SetIcd(HIS_SERVICE_REQ newServiceReq, HIS_SERVICE_REQ oldServiceReq, HIS_TREATMENT treatment)
        {
            Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
            HIS_TREATMENT old = Mapper.Map<HIS_TREATMENT>(treatment);

            if (newServiceReq.IS_MAIN_EXAM == Constant.IS_TRUE)
            {
                treatment.ICD_NAME = newServiceReq.ICD_NAME;
                treatment.ICD_CODE = CommonUtil.ToUpper(newServiceReq.ICD_CODE);
                treatment.ICD_CAUSE_CODE = CommonUtil.ToUpper(newServiceReq.ICD_CAUSE_CODE);
                treatment.ICD_CAUSE_NAME = newServiceReq.ICD_CAUSE_NAME;
            }
            
            treatment.ICD_CODE = !string.IsNullOrWhiteSpace(treatment.ICD_CODE) ? treatment.ICD_CODE.ToUpper().Replace(";", "") : null;
            treatment.ICD_NAME = !string.IsNullOrWhiteSpace(treatment.ICD_NAME) ? treatment.ICD_NAME.Replace(";", "") : null;

            //Xu ly de update lai benh phu
            List<IcdInfo> icds = HisTreatmentUpdate.GetAllIcd(newServiceReq.TREATMENT_ID, newServiceReq.ID);

            if (icds == null)
            {
                icds = new List<IcdInfo>();
            }

            IcdInfo icd = new IcdInfo();
            icd.ICD_CODE = newServiceReq.ICD_CODE;
            icd.ICD_NAME = newServiceReq.ICD_NAME;
            icd.ICD_TEXT = newServiceReq.ICD_TEXT;
            icd.ICD_SUB_CODE = newServiceReq.ICD_SUB_CODE;
            icd.ICD_TEXT = newServiceReq.ICD_TEXT;
            icds.Add(icd);

            List<string> allIcdSubCodes = new List<string>();
            List<string> allIcdTexts = new List<string>();

            if (icds != null && icds.Count > 0)
            {
                //Duyet toan bo ICD cua tat ca cac y lenh de bo sung vao treatment
                for (int i = 0; i < icds.Count; i++)
                {
                    string icdCode = !string.IsNullOrWhiteSpace(icds[i].ICD_CODE) ? icds[i].ICD_CODE.ToUpper().Replace(";", "") : null;
                    string icdName = !string.IsNullOrWhiteSpace(icds[i].ICD_NAME) ? icds[i].ICD_NAME.Replace(";", "") : null;
                    string[] icdSubCodes = !string.IsNullOrWhiteSpace(icds[i].ICD_SUB_CODE) ? icds[i].ICD_SUB_CODE.ToUpper().Replace(";;", ";").Split(';') : null;
                    string[] icdTexts = !string.IsNullOrWhiteSpace(icds[i].ICD_TEXT) ? icds[i].ICD_TEXT.Replace(";;", ";").Split(';') : null;

                    //Neu ko nam trong benh chinh hoac chua duoc add vao truoc do thi thuc hien add vao
                    if (!string.IsNullOrWhiteSpace(icdCode) && treatment.ICD_CODE != icdCode && !allIcdSubCodes.Contains(icdCode))
                    {
                        allIcdSubCodes.Add(icdCode);
                    }

                    //Neu ko nam trong benh chinh hoac chua duoc add vao truoc do thi thuc hien add vao
                    if (!string.IsNullOrWhiteSpace(icdName) && icdName != treatment.ICD_NAME && !allIcdTexts.Contains(icdName))
                    {
                        allIcdTexts.Add(icdName);
                    }

                    if (icdSubCodes != null && icdSubCodes.Length > 0)
                    {
                        for (int j = 0; j < icdSubCodes.Length; j++)
                        {
                            //Neu ko nam trong benh chinh hoac chua duoc add vao truoc do thi thuc hien add vao
                            if (!string.IsNullOrWhiteSpace(icdSubCodes[j]) && treatment.ICD_CODE != icdSubCodes[j] && !allIcdSubCodes.Contains(icdSubCodes[j]))
                            {
                                allIcdSubCodes.Add(icdSubCodes[j]);
                            }
                        }
                    }

                    if (icdTexts != null && icdTexts.Length > 0)
                    {
                        for (int j = 0; j < icdTexts.Length; j++)
                        {
                            //Neu ko nam trong benh chinh hoac chua duoc add vao truoc do thi thuc hien add vao
                            if (!string.IsNullOrWhiteSpace(icdTexts[j]) && treatment.ICD_NAME != icdTexts[j] && !allIcdTexts.Contains(icdTexts[j]))
                            {
                                allIcdTexts.Add(icdTexts[j]);
                            }
                        }
                    }
                }
            }

            if (allIcdSubCodes.Count > 0)
            {
                treatment.ICD_SUB_CODE = string.Join(";", allIcdSubCodes);
            }

            if (allIcdTexts.Count > 0)
            {
                treatment.ICD_TEXT = string.Join(";", allIcdTexts);
            }

            return Inventec.Common.ObjectChecker.ValueChecker.IsPrimitiveDiff<HIS_TREATMENT>(old, treatment);
        }


        private static List<IcdInfo> GetAllIcd(long treatmentId, long currentServiceReqId)
        {
            string sql = "SELECT ICD_NAME, ICD_CODE, ICD_SUB_CODE, ICD_TEXT FROM HIS_SERVICE_REQ REQ WHERE IS_DELETE = 0 AND REQ.TREATMENT_ID = :param1 AND REQ.ID <> :param2";
            List<IcdInfo> rs = DAOWorker.SqlDAO.GetSql<IcdInfo>(sql, treatmentId, currentServiceReqId);
            return rs;
        }

        public static bool AddIcd(HIS_TREATMENT raw, string icdSubCode, string icdText)
        {
            bool result = false;

            try
            {
                icdSubCode = HisIcdUtil.RemoveDuplicateIcd(icdSubCode);
                icdText = HisIcdUtil.RemoveDuplicateIcd(icdText);

                if (raw != null && (!string.IsNullOrWhiteSpace(icdSubCode) || !string.IsNullOrWhiteSpace(icdText)))
                {
                    HIS_ICD icd = HisIcdCFG.DATA.Where(o => o.ICD_CODE == raw.ICD_CODE).FirstOrDefault();
                    string icdCode = icd != null ? icd.ICD_CODE.ToLower() : null;
                    string icdName = raw.ICD_NAME != null ? raw.ICD_NAME.ToLower() : null;

                    if (!string.IsNullOrWhiteSpace(icdSubCode))
                    {
                        string[] icdSubCodes = icdSubCode.Split(';');
                        if (icdSubCodes != null && icdSubCodes.Length > 0)
                        {
                            foreach (string code in icdSubCodes)
                            {
                                if (!string.IsNullOrWhiteSpace(code))
                                {
                                    if (raw.ICD_SUB_CODE != null && raw.ICD_SUB_CODE.ToLower().Contains(code.ToLower()))
                                    {
                                        continue;
                                    }
                                    else if (icdCode != null && icdCode == code.ToLower())
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        raw.ICD_SUB_CODE = string.Format("{0};{1}", (raw.ICD_SUB_CODE == null ? "" : raw.ICD_SUB_CODE), code);
                                        result = true;
                                    }
                                }
                            }
                            raw.ICD_SUB_CODE = raw.ICD_SUB_CODE != null ? raw.ICD_SUB_CODE.Replace(";;", ";") : null;
                            raw.ICD_SUB_CODE = HisIcdUtil.RemoveDuplicateIcd(raw.ICD_SUB_CODE);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(icdText))
                    {
                        string[] icdTexts = icdText.Split(';');
                        if (icdTexts != null && icdTexts.Length > 0)
                        {
                            foreach (string text in icdTexts)
                            {
                                if (!string.IsNullOrWhiteSpace(text))
                                {
                                    if (raw.ICD_TEXT != null && raw.ICD_TEXT.ToLower().Contains(text.ToLower()))
                                    {
                                        continue;
                                    }
                                    else if (icdName != null && icdName.ToLower().Contains(text.ToLower()))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        raw.ICD_TEXT = string.Format("{0};{1}", (raw.ICD_TEXT == null ? "" : raw.ICD_TEXT), text);
                                        result = true;
                                    }
                                }
                            }
                            raw.ICD_TEXT = raw.ICD_TEXT != null ? raw.ICD_TEXT.Replace(";;", ";") : null;
                            raw.ICD_TEXT = HisIcdUtil.RemoveDuplicateIcd(raw.ICD_TEXT);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }
    }
}
