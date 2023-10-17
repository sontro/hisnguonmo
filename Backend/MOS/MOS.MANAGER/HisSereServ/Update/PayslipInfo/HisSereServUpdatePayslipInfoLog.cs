using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisEquipmentSet;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServ.Update.PayslipInfo
{
    partial class HisSereServUpdatePayslipInfoLog : BusinessBase
    {
        //Dic anh xa giua truong can update va message
        private static Dictionary<UpdateField, string> DIC_MESSAGE = new Dictionary<UpdateField, string>()
                {
                    {UpdateField.EQUIPMENT_SET_ORDER__AND__EQUIPMENT_SET_ID, LogCommonUtil.GetEventLogContent(EventLog.Enum.ThongTinBoVatTu)},
                    {UpdateField.IS_EXPEND, LogCommonUtil.GetEventLogContent(EventLog.Enum.ThongTinHaoPhi)},
                    {UpdateField.IS_NO_EXECUTE, LogCommonUtil.GetEventLogContent(EventLog.Enum.ThongTinKhongThucHien)},
                    {UpdateField.IS_FUND_ACCEPTED, LogCommonUtil.GetEventLogContent(EventLog.Enum.ThongTinQuyChapNhanThanhToan)},
                    {UpdateField.IS_OUT_PARENT_FEE, LogCommonUtil.GetEventLogContent(EventLog.Enum.ThongTinChiPhiNgoaiGoi)},
                    {UpdateField.PARENT_ID, LogCommonUtil.GetEventLogContent(EventLog.Enum.ThongTinDinhKemDichVu)},
                    {UpdateField.PATIENT_TYPE_ID, LogCommonUtil.GetEventLogContent(EventLog.Enum.ThongTinDoiTuongThanhToan)},
                    {UpdateField.PRIMARY_PATIENT_TYPE_ID, LogCommonUtil.GetEventLogContent(EventLog.Enum.ThongTinDoiTuongPhuThu)},
                    {UpdateField.SHARE_COUNT, LogCommonUtil.GetEventLogContent(EventLog.Enum.ThongTinSoLuongNamGhep)},
                    {UpdateField.STENT_ORDER, LogCommonUtil.GetEventLogContent(EventLog.Enum.ThongTinSoThuTuStent)},
                    {UpdateField.EXPEND_TYPE_ID, LogCommonUtil.GetEventLogContent(EventLog.Enum.ThongTinHaoPhiTienGiuong)},
                    {UpdateField.PACKAGE_PRICE, LogCommonUtil.GetEventLogContent(EventLog.Enum.ThongTinGiaGoi)},
                    {UpdateField.USER_PRICE, LogCommonUtil.GetEventLogContent(EventLog.Enum.ThongTinGiaDichVu)},
                    {UpdateField.OTHER_PAY_SOURCE_ID, LogCommonUtil.GetEventLogContent(EventLog.Enum.NguonChiTraKhac)},
                    {UpdateField.SERVICE_CONDITION_ID, LogCommonUtil.GetEventLogContent(EventLog.Enum.DieuKienDichVu)}
                };


        internal static void Log(HisSereServPayslipSDO data, List<HIS_SERE_SERV> allBefores, HIS_TREATMENT treatment)
        {
            try
            {
                string logData = "";

                List<LogSereServData> logs = new List<LogSereServData>();
                foreach (HIS_SERE_SERV ss in data.SereServs)
                {
                    LogSereServData log = new LogSereServData(ss, allBefores, data.Field);
                    logs.Add(log);
                }

                var group = logs.GroupBy(o => new { o.OldValue, o.NewValue });

                //Dinh dang vd: Chuyển đối tượng thanh toán: "BHYT" --> "Viện phí" (0000000123-Chụp xquang; 0000000125-Xét nghiệm sinh hóa;...)
                string format = "{0}: \"{1}\" ==> \"{2}\" ({3}).";
                string type = DIC_MESSAGE[data.Field];

                foreach (var g in group)
                {
                    string oldValue = g.Key.OldValue;
                    string newValue = g.Key.NewValue;
                    string detailInfos = "";
                    foreach (LogSereServData t in g.ToList())
                    {
                        detailInfos += string.Format("{0}: {1}-{2}; ", SimpleEventKey.SERVICE_REQ_CODE, t.ServiceReqCode, t.ServiceName);
                    }
                    logData += string.Format(format, type, oldValue, newValue, detailInfos);
                }

                new EventLogGenerator(EventLog.Enum.HisSereServ_SuaThongTinBangKe, logData)
                    .TreatmentCode(treatment.TREATMENT_CODE)
                    .Run();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }

    class LogSereServData
    {
        public string ServiceReqCode { get; set; }
        public string ServiceName { get; set; }
        public string ServiceCode { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public LogSereServData(string serviceReqCode, string serviceName, string serviceCode, string oldValue, string newValue)
        {
            this.ServiceReqCode = serviceReqCode;
            this.ServiceName = serviceName;
            this.ServiceCode = serviceCode;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public LogSereServData(HIS_SERE_SERV ss, List<HIS_SERE_SERV> allBefores, UpdateField field)
        {
            if (ss != null)
            {
                HIS_SERE_SERV old = allBefores != null ? allBefores.Where(o => o.ID == ss.ID).FirstOrDefault() : null;

                if (old != null)
                {
                    this.ServiceReqCode = old.TDL_SERVICE_REQ_CODE;
                    this.ServiceName = old.TDL_SERVICE_NAME;
                    this.ServiceCode = old.TDL_SERVICE_CODE;

                    if (field == UpdateField.EQUIPMENT_SET_ORDER__AND__EQUIPMENT_SET_ID)
                    {
                        List<long> ids = new List<long>();
                        if (old.EQUIPMENT_SET_ID.HasValue)
                        {
                            ids.Add(old.EQUIPMENT_SET_ID.Value);
                        }
                        if (ss.EQUIPMENT_SET_ID.HasValue)
                        {
                            ids.Add(ss.EQUIPMENT_SET_ID.Value);
                        }
                        List<HIS_EQUIPMENT_SET> equipmentSets = new HisEquipmentSetGet().GetByIds(ids);
                        string oldEquipmentSetInfo = "";
                        string newEquipmentSetInfo = "";
                        if (old.EQUIPMENT_SET_ID.HasValue)
                        {
                            HIS_EQUIPMENT_SET oldEquimentSet = equipmentSets.Where(o => o.ID == old.EQUIPMENT_SET_ID.Value).FirstOrDefault();
                            oldEquipmentSetInfo = oldEquimentSet != null ? string.Format("{0}-{1}", oldEquimentSet.EQUIPMENT_SET_NAME, old.EQUIPMENT_SET_ORDER + "") : "--";
                        }
                        if (ss.EQUIPMENT_SET_ID.HasValue)
                        {
                            HIS_EQUIPMENT_SET newEquimentSet = equipmentSets.Where(o => o.ID == old.EQUIPMENT_SET_ID.Value).FirstOrDefault();
                            newEquipmentSetInfo = newEquimentSet != null ? string.Format("{0}-{1}", newEquimentSet.EQUIPMENT_SET_NAME, ss.EQUIPMENT_SET_ORDER + "") : "--";
                        }
                        this.NewValue = newEquipmentSetInfo;
                        this.OldValue = oldEquipmentSetInfo;
                    }
                    else if (field == UpdateField.IS_EXPEND)
                    {
                        this.NewValue = ss.IS_EXPEND == Constant.IS_TRUE ? LogCommonUtil.GetEventLogContent(EventLog.Enum.LaHaoPhi) : LogCommonUtil.GetEventLogContent(EventLog.Enum.KhongPhaiHaoPhi);
                        this.OldValue = old.IS_EXPEND == Constant.IS_TRUE ? LogCommonUtil.GetEventLogContent(EventLog.Enum.LaHaoPhi) : LogCommonUtil.GetEventLogContent(EventLog.Enum.KhongPhaiHaoPhi);
                    }
                    else if (field == UpdateField.EXPEND_TYPE_ID)
                    {
                        this.NewValue = ss.EXPEND_TYPE_ID == Constant.IS_TRUE ? LogCommonUtil.GetEventLogContent(EventLog.Enum.LaHaoPhi) : LogCommonUtil.GetEventLogContent(EventLog.Enum.KhongPhaiHaoPhi);
                        this.OldValue = old.EXPEND_TYPE_ID == Constant.IS_TRUE ? LogCommonUtil.GetEventLogContent(EventLog.Enum.LaHaoPhi) : LogCommonUtil.GetEventLogContent(EventLog.Enum.KhongPhaiHaoPhi);
                    }
                    else if (field == UpdateField.IS_NO_EXECUTE)
                    {
                        this.NewValue = ss.IS_NO_EXECUTE == Constant.IS_TRUE ? LogCommonUtil.GetEventLogContent(EventLog.Enum.KhongThucHien) : LogCommonUtil.GetEventLogContent(EventLog.Enum.ThucHien);
                        this.OldValue = old.IS_NO_EXECUTE == Constant.IS_TRUE ? LogCommonUtil.GetEventLogContent(EventLog.Enum.KhongThucHien) : LogCommonUtil.GetEventLogContent(EventLog.Enum.ThucHien);
                    }
                    else if (field == UpdateField.IS_OUT_PARENT_FEE)
                    {
                        this.NewValue = ss.IS_OUT_PARENT_FEE == Constant.IS_TRUE ? LogCommonUtil.GetEventLogContent(EventLog.Enum.NgoaiChiPhiGoi) : LogCommonUtil.GetEventLogContent(EventLog.Enum.BoCheckNgoaiChiPhiGoi);
                        this.OldValue = old.IS_OUT_PARENT_FEE == Constant.IS_TRUE ? LogCommonUtil.GetEventLogContent(EventLog.Enum.NgoaiChiPhiGoi) : LogCommonUtil.GetEventLogContent(EventLog.Enum.BoCheckNgoaiChiPhiGoi);
                    }
                    else if (field == UpdateField.PARENT_ID)
                    {
                        HIS_SERE_SERV oParent = old.PARENT_ID.HasValue ? allBefores.Where(o => o.ID == old.PARENT_ID.Value).FirstOrDefault() : null;
                        HIS_SERE_SERV nParent = ss.PARENT_ID.HasValue ? allBefores.Where(o => o.ID == ss.PARENT_ID.Value).FirstOrDefault() : null;

                        this.NewValue = nParent != null ? string.Format("{0} - {1}", nParent.TDL_SERVICE_REQ_CODE, nParent.TDL_SERVICE_NAME) : "--";
                        this.OldValue = oParent != null ? string.Format("{0} - {1}", oParent.TDL_SERVICE_REQ_CODE, oParent.TDL_SERVICE_NAME) : "--";
                    }
                    else if (field == UpdateField.PATIENT_TYPE_ID)
                    {
                        HIS_PATIENT_TYPE o = HisPatientTypeCFG.DATA.Where(t => t.ID == old.PATIENT_TYPE_ID).FirstOrDefault();
                        HIS_PATIENT_TYPE n = HisPatientTypeCFG.DATA.Where(t => t.ID == ss.PATIENT_TYPE_ID).FirstOrDefault();

                        this.NewValue = n.PATIENT_TYPE_NAME;
                        this.OldValue = o.PATIENT_TYPE_NAME;
                    }
                    else if (field == UpdateField.PRIMARY_PATIENT_TYPE_ID)
                    {
                        HIS_PATIENT_TYPE o = old.PRIMARY_PATIENT_TYPE_ID.HasValue ? HisPatientTypeCFG.DATA.Where(t => t.ID == old.PRIMARY_PATIENT_TYPE_ID).FirstOrDefault() : null;
                        HIS_PATIENT_TYPE n = ss.PRIMARY_PATIENT_TYPE_ID.HasValue ? HisPatientTypeCFG.DATA.Where(t => t.ID == ss.PRIMARY_PATIENT_TYPE_ID).FirstOrDefault() : null;

                        this.NewValue = n != null ? n.PATIENT_TYPE_NAME : "";
                        this.OldValue = o != null ? o.PATIENT_TYPE_NAME : "";
                    }
                    else if (field == UpdateField.SERVICE_CONDITION_ID)
                    {
                        HIS_SERVICE_CONDITION o = old.SERVICE_CONDITION_ID.HasValue ? HisServiceConditionCFG.DATA.Where(t => t.ID == old.SERVICE_CONDITION_ID).FirstOrDefault() : null;
                        HIS_SERVICE_CONDITION n = ss.SERVICE_CONDITION_ID.HasValue ? HisServiceConditionCFG.DATA.Where(t => t.ID == ss.SERVICE_CONDITION_ID).FirstOrDefault() : null;

                        this.NewValue = n != null ? n.SERVICE_CONDITION_NAME : "";
                        this.OldValue = o != null ? o.SERVICE_CONDITION_NAME : "";
                    }
                    else if (field == UpdateField.SHARE_COUNT)
                    {
                        this.NewValue = ss.SHARE_COUNT.HasValue ? ss.SHARE_COUNT.Value + "" : "--";
                        this.OldValue = old.SHARE_COUNT.HasValue ? old.SHARE_COUNT.Value + "" : "--";
                    }
                    else if (field == UpdateField.STENT_ORDER)
                    {
                        this.NewValue = ss.STENT_ORDER.HasValue ? ss.STENT_ORDER.Value + "" : "--";
                        this.OldValue = old.STENT_ORDER.HasValue ? old.STENT_ORDER.Value + "" : "--";
                    }
                    else if (field == UpdateField.OTHER_PAY_SOURCE_ID)
                    {
                        HIS_OTHER_PAY_SOURCE o = old.OTHER_PAY_SOURCE_ID.HasValue ? HisOtherPaySourceCFG.DATA.Where(t => t.ID == old.OTHER_PAY_SOURCE_ID).FirstOrDefault() : null;
                        HIS_OTHER_PAY_SOURCE n = ss.OTHER_PAY_SOURCE_ID.HasValue ? HisOtherPaySourceCFG.DATA.Where(t => t.ID == ss.OTHER_PAY_SOURCE_ID).FirstOrDefault() : null;

                        this.NewValue = n != null ? n.OTHER_PAY_SOURCE_NAME : "";
                        this.OldValue = o != null ? o.OTHER_PAY_SOURCE_NAME : "";
                    }
                }
            }
        }
    }
}
