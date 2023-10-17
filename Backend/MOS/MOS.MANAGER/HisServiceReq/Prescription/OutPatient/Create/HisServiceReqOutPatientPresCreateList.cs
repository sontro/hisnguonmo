using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMediStock;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.MANAGER.HisServiceReq.AssignService;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.MANAGER.HisServiceReqMaty;
using MOS.MANAGER.HisServiceReqMety;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Update.Finish;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Create
{
    /// <summary>
    /// Xu ly ke don thuoc ngoai tru (don thuoc phong kham hoac don thuoc ke tu tu truc)
    /// </summary>
    class HisServiceReqOutPatientPresCreateList : BusinessBase
    {
        private List<HisServiceReqOutPatientPresCreate> processors = new List<HisServiceReqOutPatientPresCreate>();
        internal HisServiceReqOutPatientPresCreateList()
            : base()
        {
        }

        internal HisServiceReqOutPatientPresCreateList(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool Run(List<OutPatientPresSDO> listData, ref OutPatientPresResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;

                Dictionary<long, WorkPlaceSDO> dicWorkPlace = new Dictionary<long, WorkPlaceSDO>();
                Dictionary<long, HIS_TREATMENT> dicTreatment = new Dictionary<long, HIS_TREATMENT>();
                Dictionary<long, List<HIS_SERE_SERV>> dicSereServ = new Dictionary<long, List<HIS_SERE_SERV>>();
                Dictionary<long, List<HIS_PATIENT_TYPE_ALTER>> dicPats = new Dictionary<long, List<HIS_PATIENT_TYPE_ALTER>>();
                Dictionary<long, HIS_DEPARTMENT_TRAN> dicLastDt = new Dictionary<long, HIS_DEPARTMENT_TRAN>();
                Dictionary<long, HIS_PROGRAM> dicProgram = new Dictionary<long, HIS_PROGRAM>();
                Dictionary<long, V_HIS_DEATH_CERT_BOOK> dicDeathCertBook = new Dictionary<long, V_HIS_DEATH_CERT_BOOK>();
                Dictionary<long, HIS_SERVICE_REQ> dicParentServiceReq = new Dictionary<long, HIS_SERVICE_REQ>();

                foreach (var data in listData)
                {
                    WorkPlaceSDO workPlace = null;
                    HIS_TREATMENT treatment = null;
                    List<long> mediStockIds = null;
                    List<HIS_SERE_SERV> existedSereServs = null;
                    List<HIS_PATIENT_TYPE_ALTER> ptas = null;
                    HIS_DEPARTMENT_TRAN lastDt = null;
                    HIS_PROGRAM program = null;
                    V_HIS_DEATH_CERT_BOOK deathCertBook = null;
                    HIS_SERVICE_REQ parentServiceReq = null;

                    HisServiceReqPresCheck presChecker = new HisServiceReqPresCheck(param);
                    HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                    HisServiceReqOutPatientPresCheck outPatientPresChecker = new HisServiceReqOutPatientPresCheck(param);
                    HisTreatmentFinishCheck treatmentFinishChecker = new HisTreatmentFinishCheck(param);

                    this.SetServerTime(data);

                    valid = valid && presChecker.IsValidData(data);

                    valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                    valid = valid && treatmentChecker.IsUnLock(data.TreatmentId, ref treatment);
                    valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                    valid = valid && treatmentChecker.IsUnpause(treatment);
                    valid = valid && treatmentChecker.IsUnLockHein(treatment);

                    valid = valid && presChecker.IsValidExpMestReason(data.Medicines, data.Materials, false);
                    valid = valid && presChecker.IsValidIcdPatientTypeOtherPaySource(data.IcdCode, data.IcdSubCode, data.Medicines, data.Materials);
                    valid = valid && presChecker.IsValidPatientType(data, data.InstructionTime, ref ptas);
                    valid = valid && presChecker.IsAllowMediStock(data, ref mediStockIds);
                    valid = valid && presChecker.IsAllowPrescription(data);
                    valid = valid && presChecker.CheckRankPrescription(data);
                    valid = valid && outPatientPresChecker.IsValidStentAmount(data);
                    valid = valid && outPatientPresChecker.IsValidData(data, ref parentServiceReq);
                    valid = valid && presChecker.IsValidFinishTimeCls(treatment, parentServiceReq, data.InstructionTime);

                    if (valid) //valid thi moi get du lieu de tranh ton hieu nang
                    {
                        if (treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU
                            || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU
                            || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY)
                        {
                            valid = valid && presChecker.CheckAmountPrepare(treatment.ID, data.Medicines, data.Materials, null);
                        }
                        existedSereServs = new HisSereServGet().GetByTreatmentId(data.TreatmentId);
                        valid = valid && presChecker.CheckServiceFinishTime(data.InstructionTime, treatment, workPlace, existedSereServs, data.IsCabinet);
                        if (data.TreatmentFinishSDO != null)
                        {
                            data.TreatmentFinishSDO.ServiceReqId = data.ParentServiceReqId;
                            valid = valid && treatmentFinishChecker.IsValidFinishTime(data, parentServiceReq);
                            valid = valid && treatmentFinishChecker.IsValidForFinish(data.TreatmentFinishSDO, treatment, existedSereServs, ptas, ref lastDt, ref workPlace, ref program, ref deathCertBook);
                            
                            if (valid && data.ParentServiceReqId.HasValue && existedSereServs != null)
                            {
                                List<HIS_SERE_SERV> recentSereServs = existedSereServs.Where(o => o.SERVICE_REQ_ID == data.ParentServiceReqId.Value).ToList();
                                var startTime = parentServiceReq != null ? parentServiceReq.START_TIME : null;
                                var finishTime = data.TreatmentFinishSDO.TreatmentFinishTime;
                                valid = valid && treatmentFinishChecker.IsValidMinProcessTime(startTime, finishTime, recentSereServs);
                            }
                        }
                    }

                    dicWorkPlace[data.RequestRoomId] = workPlace;
                    dicTreatment[data.TreatmentId] = treatment;
                    dicSereServ[data.TreatmentId] = existedSereServs;
                    dicPats[data.TreatmentId] = ptas;
                    dicLastDt[data.TreatmentId] = lastDt;
                    dicProgram[data.TreatmentId] = program;
                    dicDeathCertBook[data.TreatmentId] = deathCertBook;
                    dicParentServiceReq[data.ParentServiceReqId ?? 0] = parentServiceReq;
                }

                if (!valid)
                    return result;

                List<OutPatientPresSDO> splitByGroupData = this.SplitPresByGroup(listData);
                List<OutPatientPresSDO> splitBySpecialData = this.SplitPresBySpecial(splitByGroupData);
                List<OutPatientPresSDO> newData = this.SplitPresByExpMestReason(splitBySpecialData);
                if (IsNotNullOrEmpty(newData))
                {
                    resultData = new OutPatientPresResultSDO();
                    resultData.ExpMests = new List<HIS_EXP_MEST>();
                    resultData.Materials = new List<HIS_EXP_MEST_MATERIAL>();
                    resultData.Medicines = new List<HIS_EXP_MEST_MEDICINE>();
                    resultData.ServiceReqMaties = new List<HIS_SERVICE_REQ_MATY>();
                    resultData.ServiceReqMeties = new List<HIS_SERVICE_REQ_METY>();
                    resultData.ServiceReqs = new List<HIS_SERVICE_REQ>();
                    resultData.SereServs = new List<HIS_SERE_SERV>();

                    for (int i = 0; i < newData.Count; i++)
                    {
                        OutPatientPresSDO s = newData[i];

                        //Trong truong hop co nhieu don thi chi truyen thong tin ket thuc o don cuoi cung de
                        //tranh viec tao don truoc ket hop ket thuc ho so thi cac don sau se khong tao duoc
                        if (i < newData.Count - 1)
                        {
                            s.TreatmentFinishSDO = null;
                        }

                        List<long> mediStockIds = GetMediStock(s);

                        OutPatientPresResultSDO rs = new OutPatientPresResultSDO();
                        HisServiceReqOutPatientPresCreate processor = new HisServiceReqOutPatientPresCreate(param);
                        this.processors.Add(processor);

                        if (!processor.RunWithoutValidate(s, dicWorkPlace[s.RequestRoomId], Guid.NewGuid().ToString(), dicTreatment[s.TreatmentId], dicParentServiceReq[s.ParentServiceReqId ?? 0], dicSereServ[s.TreatmentId], mediStockIds, dicPats[s.TreatmentId], dicLastDt[s.TreatmentId], dicProgram[s.TreatmentId], dicDeathCertBook[s.TreatmentId], ref rs))
                        {
                            throw new Exception("Rollback");
                        }
                        if (IsNotNullOrEmpty(rs.ExpMests))
                        {
                            resultData.ExpMests.AddRange(rs.ExpMests);
                        }
                        if (IsNotNullOrEmpty(rs.Materials))
                        {
                            resultData.Materials.AddRange(rs.Materials);
                        }
                        if (IsNotNullOrEmpty(rs.Medicines))
                        {
                            resultData.Medicines.AddRange(rs.Medicines);
                        }
                        if (IsNotNullOrEmpty(rs.ServiceReqMaties))
                        {
                            resultData.ServiceReqMaties.AddRange(rs.ServiceReqMaties);
                        }
                        if (IsNotNullOrEmpty(rs.ServiceReqMeties))
                        {
                            resultData.ServiceReqMeties.AddRange(rs.ServiceReqMeties);
                        }
                        if (IsNotNullOrEmpty(rs.ServiceReqs))
                        {
                            resultData.ServiceReqs.AddRange(rs.ServiceReqs);
                        }
                        if (IsNotNullOrEmpty(rs.SereServs))
                        {
                            resultData.SereServs.AddRange(rs.SereServs);
                        }

                        //cap nhat lai tat ca dich vu cua ho so
                        dicSereServ[s.TreatmentId] = new HisSereServGet().GetByTreatmentId(s.TreatmentId);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                resultData = null;
                result = false;
            }
            return result;
        }

        private List<long> GetMediStock(PrescriptionSDO data)
        {
            List<long> tmp = new List<long>();

            if (IsNotNullOrEmpty(data.Medicines))
            {
                List<long> ids = data.Medicines.Select(o => o.MediStockId).Distinct().ToList();
                tmp.AddRange(ids);
            }
            if (IsNotNullOrEmpty(data.Materials))
            {
                List<long> ids = data.Materials.Select(o => o.MediStockId).Distinct().ToList();
                tmp.AddRange(ids);
            }
            if (IsNotNullOrEmpty(data.SerialNumbers))
            {
                List<long> ids = data.SerialNumbers.Select(o => o.MediStockId).Distinct().ToList();
                tmp.AddRange(ids);
            }

            tmp = tmp.Distinct().ToList();

            return tmp;
        }

        private List<OutPatientPresSDO> SplitPresBySpecial(List<OutPatientPresSDO> data)
        {
            List<OutPatientPresSDO> resultData = new List<OutPatientPresSDO>();
            try
            {
                if (IsNotNullOrEmpty(data))
                {
                    if (HisExpMestCFG.SPECIAL_MEDICINE_NUM_ORDER_OPTION == HisExpMestCFG.SpecialMedicineNumOrderOption.BY_YEAR__TYPE__REQ_DEPARTMENT__MEDI_STOCK)
                    {
                        foreach (OutPatientPresSDO s in data)
                        {
                            List<OutPatientPresSDO> n = this.SplitPresBySpecialMedicineOption(s);
                            if (IsNotNullOrEmpty(n))
                            {
                                resultData.AddRange(n);
                            }
                        }
                    }
                    else
                    {
                        resultData = data;
                    }
                }
            }
            catch (Exception ex)
            {
                resultData = null;
                LogSystem.Error(ex);
            }
            return resultData;
        }

        private List<OutPatientPresSDO> SplitPresByExpMestReason(List<OutPatientPresSDO> data)
        {
            List<OutPatientPresSDO> resultData = new List<OutPatientPresSDO>();
            try
            {
                if (IsNotNullOrEmpty(data))
                {
                    foreach (var sdo in data)
                    {
                        List<long?> reasons = new List<long?>();
                        if (IsNotNullOrEmpty(sdo.Medicines))
                            reasons.AddRange(sdo.Medicines.Select(o => o.ExpMestReasonId).ToList());
                        if (IsNotNullOrEmpty(sdo.Materials))
                            reasons.AddRange(sdo.Materials.Select(o => o.ExpMestReasonId).ToList());

                        if (IsNotNullOrEmpty(sdo.ServiceReqMeties))
                            reasons.AddRange(sdo.ServiceReqMeties.Select(o => o.ExpMestReasonId).ToList());
                        if (IsNotNullOrEmpty(sdo.ServiceReqMaties))
                            reasons.AddRange(sdo.ServiceReqMaties.Select(o => o.ExpMestReasonId).ToList());

                        if (IsNotNullOrEmpty(reasons))
                        {
                            reasons = reasons.Distinct().ToList();
                            foreach (var reason in reasons)
                            {
                                Mapper.CreateMap<OutPatientPresSDO, OutPatientPresSDO>();
                                OutPatientPresSDO newSDO = Mapper.Map<OutPatientPresSDO>(sdo);

                                newSDO.Medicines = IsNotNullOrEmpty(sdo.Medicines) ? sdo.Medicines.Where(o => o.ExpMestReasonId == reason).ToList() : null;
                                newSDO.Materials = IsNotNullOrEmpty(sdo.Materials) ? sdo.Materials.Where(o => o.ExpMestReasonId == reason).ToList() : null;
                                newSDO.ServiceReqMeties = IsNotNullOrEmpty(sdo.ServiceReqMeties) ? sdo.ServiceReqMeties.Where(o => o.ExpMestReasonId == reason).ToList() : null;
                                newSDO.ServiceReqMaties = IsNotNullOrEmpty(sdo.ServiceReqMaties) ? sdo.ServiceReqMaties.Where(o => o.ExpMestReasonId == reason).ToList() : null;
                                resultData.Add(newSDO);
                            }
                            resultData = resultData.OrderBy(o => o.UseTime).ToList();
                        }
                        else
                        {
                            resultData.Add(sdo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultData = null;
                LogSystem.Error(ex);
            }
            return resultData;
        }

        private void RollbackData()
        {
            try
            {
                if (IsNotNullOrEmpty(this.processors))
                {
                    foreach (HisServiceReqOutPatientPresCreate p in this.processors)
                    {
                        p.RollbackData();
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
        }

        private List<OutPatientPresSDO> SplitPresByGroup(List<OutPatientPresSDO> datas)
        {
            List<OutPatientPresSDO> resultData = new List<OutPatientPresSDO>();
            try
            {
                if (IsNotNullOrEmpty(datas))
                {
                    foreach (var data in datas)
                    {
                        List<PresMedicineSDO> huongThan = null;
                        List<PresMedicineSDO> gayNghien = null;
                        List<PresMedicineSDO> thuocThuong = null;
                        List<PresMedicineSDO> thucPham = null;
                        List<PresOutStockMetySDO> huongThan2 = null;
                        List<PresOutStockMetySDO> gayNghien2 = null;
                        List<PresOutStockMetySDO> thucPham2 = null;
                        List<PresOutStockMetySDO> thuocThuong2 = null;

                        if (HisServiceReqCFG.SPLIT_PRES_BY_GROUP_OPTION == HisServiceReqCFG.SpitPresByByGroupOption.OPT1
                            || HisServiceReqCFG.SPLIT_PRES_BY_GROUP_OPTION == HisServiceReqCFG.SpitPresByByGroupOption.OPT2)
                        {
                            new HisServiceReqOutPatientPresUtil().SplitByGroup(data, ref huongThan, ref gayNghien, ref thuocThuong, ref thucPham, ref huongThan2, ref gayNghien2, ref thucPham2, ref thuocThuong2);

                            List<OutPatientPresSDO> newData = new List<OutPatientPresSDO>();
                            Mapper.CreateMap<OutPatientPresSDO, OutPatientPresSDO>();
                            if (IsNotNullOrEmpty(huongThan) || IsNotNullOrEmpty(huongThan2))
                            {
                                OutPatientPresSDO sdo = Mapper.Map<OutPatientPresSDO>(data);
                                sdo.Medicines = huongThan;
                                sdo.ServiceReqMeties = huongThan2;
                                sdo.SerialNumbers = null;
                                sdo.ServiceReqMaties = null;
                                sdo.Materials = null;
                                sdo.PresGroup = (long)HisServiceReqCFG.PresGroup.HUONG_THAN;
                                resultData.Add(sdo);
                            }

                            if (IsNotNullOrEmpty(gayNghien) || IsNotNullOrEmpty(gayNghien2))
                            {
                                OutPatientPresSDO sdo = Mapper.Map<OutPatientPresSDO>(data);
                                sdo.Medicines = gayNghien;
                                sdo.ServiceReqMeties = gayNghien2;
                                sdo.SerialNumbers = null;
                                sdo.ServiceReqMaties = null;
                                sdo.Materials = null;
                                sdo.PresGroup = (long)HisServiceReqCFG.PresGroup.GAY_NGHIEN;
                                resultData.Add(sdo);
                            }

                            if (IsNotNullOrEmpty(data.SerialNumbers))
                            {
                                OutPatientPresSDO sdo = Mapper.Map<OutPatientPresSDO>(data);
                                sdo.Medicines = null;
                                sdo.ServiceReqMeties = null;
                                sdo.SerialNumbers = data.SerialNumbers;
                                sdo.ServiceReqMaties = null;
                                sdo.Materials = null;
                                resultData.Add(sdo);
                            }

                            if (HisServiceReqCFG.SPLIT_PRES_BY_GROUP_OPTION == HisServiceReqCFG.SpitPresByByGroupOption.OPT1)
                            {
                                if (IsNotNullOrEmpty(thuocThuong) || IsNotNullOrEmpty(thuocThuong2))
                                {
                                    OutPatientPresSDO sdo = Mapper.Map<OutPatientPresSDO>(data);
                                    sdo.Medicines = thuocThuong;
                                    sdo.ServiceReqMeties = thuocThuong2;
                                    sdo.SerialNumbers = null;
                                    sdo.ServiceReqMaties = null;
                                    sdo.Materials = null;
                                    sdo.PresGroup = (long)HisServiceReqCFG.PresGroup.THUONG;
                                    resultData.Add(sdo);
                                }

                                if (IsNotNullOrEmpty(data.Materials) || IsNotNullOrEmpty(data.ServiceReqMaties)
                                    || IsNotNullOrEmpty(thucPham) || IsNotNullOrEmpty(thucPham2))
                                {
                                    OutPatientPresSDO sdo = Mapper.Map<OutPatientPresSDO>(data);
                                    sdo.Medicines = thucPham;
                                    sdo.ServiceReqMeties = thucPham2;
                                    sdo.SerialNumbers = null;
                                    sdo.ServiceReqMaties = data.ServiceReqMaties;
                                    sdo.Materials = data.Materials;
                                    sdo.PresGroup = (long)HisServiceReqCFG.PresGroup.HO_TRO;
                                    resultData.Add(sdo);
                                }
                            }
                            else if (HisServiceReqCFG.SPLIT_PRES_BY_GROUP_OPTION == HisServiceReqCFG.SpitPresByByGroupOption.OPT2)
                            {
                                if (IsNotNullOrEmpty(thuocThuong) || IsNotNullOrEmpty(thuocThuong2) || IsNotNullOrEmpty(data.Materials) || IsNotNullOrEmpty(data.ServiceReqMaties))
                                {
                                    OutPatientPresSDO sdo = Mapper.Map<OutPatientPresSDO>(data);
                                    sdo.Medicines = thuocThuong;
                                    sdo.ServiceReqMeties = thuocThuong2;
                                    sdo.SerialNumbers = null;
                                    sdo.ServiceReqMaties = data.ServiceReqMaties;
                                    sdo.Materials = data.Materials;
                                    sdo.PresGroup = (long)HisServiceReqCFG.PresGroup.THUONG;
                                    resultData.Add(sdo);
                                }

                                if (IsNotNullOrEmpty(thucPham) || IsNotNullOrEmpty(thucPham2))
                                {
                                    OutPatientPresSDO sdo = Mapper.Map<OutPatientPresSDO>(data);
                                    sdo.Medicines = thucPham;
                                    sdo.ServiceReqMeties = thucPham2;
                                    sdo.SerialNumbers = null;

                                    sdo.Materials = null;
                                    sdo.ServiceReqMaties = null;

                                    sdo.PresGroup = (long)HisServiceReqCFG.PresGroup.HO_TRO;
                                    resultData.Add(sdo);
                                }
                            }
                        }
                        else
                        {
                            resultData.Add(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultData = null;
                LogSystem.Error(ex);
            }
            return resultData;
        }

        private List<OutPatientPresSDO> SplitPresBySpecialMedicineOption(OutPatientPresSDO data)
        {
            //Neu co cau hinh danh so xuat thuoc dac biet thi can xu ly de kiem tra de tach don thuoc nhap dam bao 1 don chi chua thuoc gay nghien/huong than hoac thuoc doc hoac thuoc/vat tu khac
            //Khong can kiem tra voi thuoc/vat tu ke ngoai kho
            if (HisExpMestCFG.SPECIAL_MEDICINE_NUM_ORDER_OPTION == HisExpMestCFG.SpecialMedicineNumOrderOption.BY_YEAR__TYPE__REQ_DEPARTMENT__MEDI_STOCK && data != null)
            {
                Mapper.CreateMap<OutPatientPresSDO, OutPatientPresSDO>();

                List<OutPatientPresSDO> newData = new List<OutPatientPresSDO>();

                List<PresMedicineSDO> gayNghienHuongThan = IsNotNullOrEmpty(data.Medicines) ? data.Medicines.Where(t => (HisMedicineTypeCFG.HUONG_THAN_IDs != null && HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId)) || (HisMedicineTypeCFG.GAY_NGHIEN_IDs != null && HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId))).ToList() : null;

                List<PresMedicineSDO> thuocDoc = IsNotNullOrEmpty(data.Medicines) ? data.Medicines.Where(t => (HisMedicineTypeCFG.THUOC_DOC_IDs != null && HisMedicineTypeCFG.THUOC_DOC_IDs.Contains(t.MedicineTypeId))).ToList() : null;

                List<PresMedicineSDO> thuocKhac = IsNotNullOrEmpty(data.Medicines) ? data.Medicines.Where(t =>
                    (HisMedicineTypeCFG.THUOC_DOC_IDs == null || !HisMedicineTypeCFG.THUOC_DOC_IDs.Contains(t.MedicineTypeId))
                    && (HisMedicineTypeCFG.HUONG_THAN_IDs == null || !HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId))
                    && (HisMedicineTypeCFG.GAY_NGHIEN_IDs == null || !HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId))
                    ).ToList() : null;

                List<PresOutStockMetySDO> gayNghienHuongThanNgoaiKho = IsNotNullOrEmpty(data.ServiceReqMeties) ? data.ServiceReqMeties.Where(t => (HisMedicineTypeCFG.HUONG_THAN_IDs != null && t.MedicineTypeId.HasValue && HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId.Value)) || (HisMedicineTypeCFG.GAY_NGHIEN_IDs != null && t.MedicineTypeId.HasValue && HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId.Value))).ToList() : null;

                List<PresOutStockMetySDO> thuocDocNgoaiKho = IsNotNullOrEmpty(data.ServiceReqMeties) ? data.ServiceReqMeties.Where(t => t.MedicineTypeId.HasValue && (HisMedicineTypeCFG.THUOC_DOC_IDs != null && HisMedicineTypeCFG.THUOC_DOC_IDs.Contains(t.MedicineTypeId.Value))).ToList() : null;

                List<PresOutStockMetySDO> thuocKhacNgoaiKho = IsNotNullOrEmpty(data.ServiceReqMeties) ? data.ServiceReqMeties.Where(t => !t.MedicineTypeId.HasValue
                    || ((HisMedicineTypeCFG.THUOC_DOC_IDs == null || !HisMedicineTypeCFG.THUOC_DOC_IDs.Contains(t.MedicineTypeId.Value))
                    && (HisMedicineTypeCFG.HUONG_THAN_IDs == null || !HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId.Value))
                    && (HisMedicineTypeCFG.GAY_NGHIEN_IDs == null || !HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId.Value)))
                    ).ToList() : null;

                bool gnht = IsNotNullOrEmpty(gayNghienHuongThan) || IsNotNullOrEmpty(gayNghienHuongThanNgoaiKho);
                bool doc = IsNotNullOrEmpty(thuocDoc) || IsNotNullOrEmpty(thuocDocNgoaiKho);
                bool khac = IsNotNullOrEmpty(thuocKhac) || IsNotNullOrEmpty(thuocKhacNgoaiKho) || IsNotNullOrEmpty(data.ServiceReqMaties) || IsNotNullOrEmpty(data.Materials) || IsNotNullOrEmpty(data.SerialNumbers);

                if (khac || gnht || doc)
                {
                    if (khac)
                    {
                        OutPatientPresSDO sdo = Mapper.Map<OutPatientPresSDO>(data);
                        sdo.ServiceReqMaties = data.ServiceReqMaties;
                        sdo.ServiceReqMeties = thuocKhacNgoaiKho;
                        sdo.SerialNumbers = data.SerialNumbers;
                        sdo.Materials = data.Materials;
                        sdo.Medicines = thuocKhac;
                        newData.Add(sdo);
                    }
                    if (gnht)
                    {
                        OutPatientPresSDO sdo = Mapper.Map<OutPatientPresSDO>(data);
                        sdo.Materials = null;
                        sdo.SerialNumbers = null;
                        sdo.ServiceReqMaties = null;
                        sdo.ServiceReqMeties = gayNghienHuongThanNgoaiKho;
                        sdo.Medicines = gayNghienHuongThan;
                        sdo.SpecialMedicineType = (long?)HisExpMestCFG.SpecialMedicineType.GN_HT;
                        newData.Add(sdo);
                    }
                    if (doc)
                    {
                        OutPatientPresSDO sdo = Mapper.Map<OutPatientPresSDO>(data);
                        sdo.Materials = null;
                        sdo.SerialNumbers = null;
                        sdo.ServiceReqMaties = null;
                        sdo.ServiceReqMeties = thuocDocNgoaiKho;
                        sdo.Medicines = thuocDoc;
                        sdo.SpecialMedicineType = (long?)HisExpMestCFG.SpecialMedicineType.DOC;
                        newData.Add(sdo);
                    }

                    return newData;
                }
            }
            return new List<OutPatientPresSDO>() { data };
        }

        /// <summary>
        /// Xu ly de gan cac thong tin thoi gian theo gio server
        /// </summary>
        /// <param name="data"></param>
        private void SetServerTime(OutPatientPresSDO data)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            if (SystemCFG.IS_USING_SERVER_TIME && data != null)
            {
                long now = Inventec.Common.DateTime.Get.Now().Value;
                data.InstructionTime = now;

                if (data.TreatmentFinishSDO != null)
                {
                    data.TreatmentFinishSDO.TreatmentFinishTime = now;
                }
            }
        }
    }
}
