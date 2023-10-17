using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.UTILITY;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMedicine;

namespace MOS.MANAGER.HisServiceReq.UpdateCommonInfo
{
    partial class HisServiceReqUpdateCommonInfoCheck : BusinessBase
    {
        internal HisServiceReqUpdateCommonInfoCheck()
            : base()
        {
        }

        internal HisServiceReqUpdateCommonInfoCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool IsValidData(HIS_SERVICE_REQ data, HIS_SERVICE_REQ old)
        {
            bool result = true;

            try
            {
                //Neu bo tick thi kiem tra xem y lenh cha co tick "ko huong BHYT" ko
                if (old.IS_NOT_USE_BHYT == Constant.IS_TRUE && data.IS_NOT_USE_BHYT != Constant.IS_TRUE && old.PARENT_ID.HasValue)
                {
                    HIS_SERVICE_REQ parent = new HisServiceReqGet().GetById(old.PARENT_ID.Value);
                    if (parent.IS_NOT_USE_BHYT == Constant.IS_TRUE)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhChaKhongHuongBHYT, parent.SERVICE_REQ_CODE);
                        return false;
                    }
                }

                //Neu tick thi kiem tra xem trong y lenh co dich vu nao co doi tuong thanh toan la BHYT ko
                if (old.IS_NOT_USE_BHYT != Constant.IS_TRUE && data.IS_NOT_USE_BHYT == Constant.IS_TRUE)
                {
                    HisSereServFilterQuery filter = new HisSereServFilterQuery();
                    filter.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;
                    filter.SERVICE_REQ_ID = old.ID;

                    List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().Get(filter);
                    if (IsNotNullOrEmpty(hisSereServs))
                    {
                        List<string> serviceCodes = hisSereServs.Select(o => o.TDL_SERVICE_CODE).ToList();
                        string codeStr = string.Join(",", serviceCodes);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CacDichVuCoDoiTuongThanhToanLaBHYT, codeStr);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }

            return result;
        }

        internal bool IsValidPresBidDate(HIS_SERVICE_REQ data, HIS_SERVICE_REQ old)
        {
            bool result = true;

            try
            {
                if (HisServiceReqCFG.DO_NOT_ALLOW_PRES_WHEN_INTRUCTION_TIME_OUT_OF_BID_VALID_DATE &&
                    old.INTRUCTION_TIME != data.INTRUCTION_TIME &&
                    (data.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONDT
                    || data.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK
                    || data.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONTT))
                {
                    HisExpMestMaterialFilterQuery expMestMaterialFilter = new HisExpMestMaterialFilterQuery();
                    expMestMaterialFilter.TDL_SERVICE_REQ_ID = data.ID;
                    List<HIS_EXP_MEST_MATERIAL> listExpMestMaterial = new HisExpMestMaterialGet(param).Get(expMestMaterialFilter);

                    HisExpMestMedicineFilterQuery expMestMedicineFilter = new HisExpMestMedicineFilterQuery();
                    expMestMedicineFilter.TDL_SERVICE_REQ_ID = data.ID;
                    List<HIS_EXP_MEST_MEDICINE> listExpMestMedicine = new HisExpMestMedicineGet(param).Get(expMestMedicineFilter);

                    List<string> error = new List<string>();

                    if (IsNotNullOrEmpty(listExpMestMaterial) || IsNotNullOrEmpty(listExpMestMedicine))
                    {
                        Dictionary<string, List<string>> dicFromTo = new Dictionary<string, List<string>>();
                        Dictionary<string, List<string>> dicFrom = new Dictionary<string, List<string>>();
                        Dictionary<string, List<string>> dicTo = new Dictionary<string, List<string>>();

                        long intructionDate = data.INTRUCTION_TIME - data.INTRUCTION_TIME % 1000000;

                        if (IsNotNullOrEmpty(listExpMestMaterial))
                        {
                            List<long> materialIds = listExpMestMaterial.Select(s => s.MATERIAL_ID ?? 0).Distinct().ToList();
                            List<V_HIS_MATERIAL_2> listMaterial = new HisMaterialGet().GetView2ByIds(materialIds);
                            listMaterial = listMaterial.Where(o => o.VALID_FROM_TIME.HasValue || o.VALID_TO_TIME.HasValue).ToList();
                            if (IsNotNullOrEmpty(listMaterial))
                            {
                                foreach (var material in listMaterial)
                                {
                                    string materialName = string.Format("{0}({1})", material.MATERIAL_TYPE_NAME, material.MATERIAL_TYPE_CODE);

                                    if (material.VALID_FROM_TIME.HasValue && material.VALID_TO_TIME.HasValue &&
                                        (intructionDate < material.VALID_FROM_TIME.Value || intructionDate > material.VALID_TO_TIME.Value))
                                    {
                                        string validFromTime = Inventec.Common.DateTime.Convert.TimeNumberToDateString(material.VALID_FROM_TIME.Value);
                                        string validToTime = Inventec.Common.DateTime.Convert.TimeNumberToDateString(material.VALID_TO_TIME.Value);
                                        string key = string.Format("{0} - {1}", validFromTime, validToTime);

                                        if (!dicFromTo.ContainsKey(key))
                                        {
                                            dicFromTo[key] = new List<string>();
                                        }

                                        dicFromTo[key].Add(materialName);
                                    }
                                    else if (material.VALID_FROM_TIME.HasValue && intructionDate < material.VALID_FROM_TIME.Value)
                                    {
                                        string validFromTime = Inventec.Common.DateTime.Convert.TimeNumberToDateString(material.VALID_FROM_TIME.Value);

                                        if (!dicFrom.ContainsKey(validFromTime))
                                        {
                                            dicFrom[validFromTime] = new List<string>();
                                        }

                                        dicFrom[validFromTime].Add(materialName);
                                    }
                                    else if (material.VALID_TO_TIME.HasValue && intructionDate > material.VALID_TO_TIME.Value)
                                    {
                                        string validToTime = Inventec.Common.DateTime.Convert.TimeNumberToDateString(material.VALID_TO_TIME.Value);

                                        if (!dicTo.ContainsKey(validToTime))
                                        {
                                            dicTo[validToTime] = new List<string>();
                                        }

                                        dicTo[validToTime].Add(materialName);
                                    }
                                }
                            }
                        }

                        if (IsNotNullOrEmpty(listExpMestMedicine))
                        {
                            List<long> medicineIds = listExpMestMedicine.Select(s => s.MEDICINE_ID ?? 0).Distinct().ToList();
                            List<V_HIS_MEDICINE_2> listMedicine = new HisMedicineGet().GetView2ByIds(medicineIds);
                            listMedicine = listMedicine.Where(o => o.VALID_FROM_TIME.HasValue || o.VALID_TO_TIME.HasValue).ToList();
                            if (IsNotNullOrEmpty(listMedicine))
                            {
                                foreach (var medicine in listMedicine)
                                {
                                    string medicineName = string.Format("{0}({1})", medicine.MEDICINE_TYPE_NAME, medicine.MEDICINE_TYPE_CODE);
                                    if (medicine.VALID_FROM_TIME.HasValue && medicine.VALID_TO_TIME.HasValue &&
                                        (intructionDate < medicine.VALID_FROM_TIME.Value || intructionDate > medicine.VALID_TO_TIME.Value))
                                    {
                                        string validFromTime = Inventec.Common.DateTime.Convert.TimeNumberToDateString(medicine.VALID_FROM_TIME.Value);
                                        string validToTime = Inventec.Common.DateTime.Convert.TimeNumberToDateString(medicine.VALID_TO_TIME.Value);
                                        string key = string.Format("{0} - {1}", validFromTime, validToTime);

                                        if (!dicFromTo.ContainsKey(key))
                                        {
                                            dicFromTo[key] = new List<string>();
                                        }

                                        dicFromTo[key].Add(medicineName);
                                    }
                                    else if (medicine.VALID_FROM_TIME.HasValue && intructionDate < medicine.VALID_FROM_TIME.Value)
                                    {
                                        string validFromTime = Inventec.Common.DateTime.Convert.TimeNumberToDateString(medicine.VALID_FROM_TIME.Value);

                                        if (!dicFrom.ContainsKey(validFromTime))
                                        {
                                            dicFrom[validFromTime] = new List<string>();
                                        }

                                        dicFrom[validFromTime].Add(medicineName);
                                    }
                                    else if (medicine.VALID_TO_TIME.HasValue && intructionDate > medicine.VALID_TO_TIME.Value)
                                    {
                                        string validToTime = Inventec.Common.DateTime.Convert.TimeNumberToDateString(medicine.VALID_TO_TIME.Value);

                                        if (!dicTo.ContainsKey(validToTime))
                                        {
                                            dicTo[validToTime] = new List<string>();
                                        }

                                        dicTo[validToTime].Add(medicineName);
                                    }
                                }
                            }
                        }

                        if (dicFrom.Count > 0)
                        {
                            string mess = MessageUtil.GetMessage(MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianYlenhNhoHonNgayHieuLucThau, param.LanguageCode);

                            foreach (var item in dicFrom)
                            {
                                error.Add(string.Format(mess, string.Join("; ", item.Value.Distinct()), item.Key));
                            }
                        }

                        if (dicTo.Count > 0)
                        {
                            string mess = MessageUtil.GetMessage(MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianYlenhLonHonNgayHieuLucThau, param.LanguageCode);

                            foreach (var item in dicTo)
                            {
                                error.Add(string.Format(mess, string.Join("; ", item.Value.Distinct()), item.Key));
                            }
                        }

                        if (dicFromTo.Count > 0)
                        {
                            string mess = MessageUtil.GetMessage(MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianYlenhNgoaiKhoangNgayHieuLucThau, param.LanguageCode);

                            foreach (var item in dicFromTo)
                            {
                                error.Add(string.Format(mess, string.Join("; ", item.Value.Distinct()), item.Key));
                            }
                        }
                    }

                    if (IsNotNullOrEmpty(error))
                    {
                        param.Messages.AddRange(error);
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }

            return result;
        }
    }
}
