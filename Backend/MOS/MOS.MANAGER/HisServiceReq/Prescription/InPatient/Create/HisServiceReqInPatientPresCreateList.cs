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

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient.Create
{
    /// <summary>
    /// Xu ly ke don thuoc dieu tri
    /// </summary>
    class HisServiceReqInPatientPresCreateList : BusinessBase
    {
        private List<HisServiceReqInPatientPresCreate> processors = new List<HisServiceReqInPatientPresCreate>();

        internal HisServiceReqInPatientPresCreateList()
            : base()
        {
        }

        internal HisServiceReqInPatientPresCreateList(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool Run(InPatientPresSDO data, ref InPatientPresResultSDO resultData)
        {
            bool result = false;
            try
            {
                HisServiceReqPresCheck presChecker = new HisServiceReqPresCheck(param);
                bool valid = true;
                valid = valid && presChecker.IsValidExpMestReason(data.Medicines, data.Materials, false);
                valid = valid && presChecker.IsValidIcdPatientTypeOtherPaySource(data.IcdCode, data.IcdSubCode, data.Medicines, data.Materials);

                if (!valid)
                    return result;

                List<InPatientPresSDO> SplitData = this.SplitPresBySpecialMedicineOption(data);
                List<InPatientPresSDO> SplitDataByReason = this.SplitPresByExpMestReason(SplitData);
                List<InPatientPresSDO> newData = this.SplitPresByUseTimes(SplitDataByReason);
                if (IsNotNullOrEmpty(newData))
                {
                    resultData = new InPatientPresResultSDO();
                    resultData.ExpMests = new List<HIS_EXP_MEST>();
                    resultData.Materials = new List<HIS_EXP_MEST_MATERIAL>();
                    resultData.Medicines = new List<HIS_EXP_MEST_MEDICINE>();
                    resultData.ServiceReqMaties = new List<HIS_SERVICE_REQ_MATY>();
                    resultData.ServiceReqMeties = new List<HIS_SERVICE_REQ_METY>();
                    resultData.ServiceReqs = new List<HIS_SERVICE_REQ>();
                    resultData.SereServs = new List<HIS_SERE_SERV>();

                    for (int i = 0; i < newData.Count; i++)
                    {
                        InPatientPresSDO s = newData[i];

                        InPatientPresResultSDO rs = new InPatientPresResultSDO();
                        HisServiceReqInPatientPresCreate processor = new HisServiceReqInPatientPresCreate(param);
                        this.processors.Add(processor);

                        if (!processor.Create(s, ref rs))
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

        private List<InPatientPresSDO> SplitPresByExpMestReason(List<InPatientPresSDO> data)
        {
            List<InPatientPresSDO> resultData = new List<InPatientPresSDO>();
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
                        if (IsNotNullOrEmpty(reasons))
                        {
                            reasons = reasons.Distinct().ToList();
                            foreach (var reason in reasons)
                            {
                                Mapper.CreateMap<InPatientPresSDO, InPatientPresSDO>();
                                InPatientPresSDO newSDO = Mapper.Map<InPatientPresSDO>(sdo);

                                newSDO.Medicines = IsNotNullOrEmpty(sdo.Medicines) ? sdo.Medicines.Where(o => o.ExpMestReasonId == reason).ToList() : null;
                                newSDO.Materials = IsNotNullOrEmpty(sdo.Materials) ? sdo.Materials.Where(o => o.ExpMestReasonId == reason).ToList() : null;
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

        private List<InPatientPresSDO> SplitPresByUseTimes(List<InPatientPresSDO> data)
        {
            List<InPatientPresSDO> resultData = new List<InPatientPresSDO>();
            try
            {
                if (IsNotNullOrEmpty(data))
                {
                    foreach (var sdo in data)
                    {
                        if (IsNotNullOrEmpty(sdo.UseTimes))
                        {
                            foreach (var usetime in sdo.UseTimes)
                            {
                                Mapper.CreateMap<InPatientPresSDO, InPatientPresSDO>();
                                InPatientPresSDO newSDO = Mapper.Map<InPatientPresSDO>(sdo);
                                newSDO.UseTimes = null;
                                newSDO.UseTime = usetime;
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
                    foreach (HisServiceReqInPatientPresCreate p in this.processors)
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

        private List<InPatientPresSDO> SplitPresBySpecialMedicineOption(InPatientPresSDO data)
        {
            //Neu co cau hinh danh so xuat thuoc dac biet thi can xu ly de kiem tra de tach don thuoc nhap dam bao 1 don chi chua thuoc gay nghien/huong than hoac thuoc doc hoac thuoc/vat tu khac
            //Khong can kiem tra voi thuoc/vat tu ke ngoai kho
            if (HisExpMestCFG.SPECIAL_MEDICINE_NUM_ORDER_OPTION == HisExpMestCFG.SpecialMedicineNumOrderOption.BY_YEAR__TYPE__REQ_DEPARTMENT__MEDI_STOCK && data != null)
            {
                Mapper.CreateMap<InPatientPresSDO, InPatientPresSDO>();

                List<InPatientPresSDO> newData = new List<InPatientPresSDO>();

                List<PresMedicineSDO> gayNghienHuongThan = IsNotNullOrEmpty(data.Medicines) ? data.Medicines.Where(t => (HisMedicineTypeCFG.HUONG_THAN_IDs != null && HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId)) || (HisMedicineTypeCFG.GAY_NGHIEN_IDs != null && HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId))).ToList() : null;

                List<PresMedicineSDO> thuocDoc = IsNotNullOrEmpty(data.Medicines) ? data.Medicines.Where(t => (HisMedicineTypeCFG.THUOC_DOC_IDs != null && HisMedicineTypeCFG.THUOC_DOC_IDs.Contains(t.MedicineTypeId))).ToList() : null;

                List<PresMedicineSDO> thuocKhac = IsNotNullOrEmpty(data.Medicines) ? data.Medicines.Where(t => (HisMedicineTypeCFG.THUOC_DOC_IDs == null || !HisMedicineTypeCFG.THUOC_DOC_IDs.Contains(t.MedicineTypeId))
                    && (HisMedicineTypeCFG.HUONG_THAN_IDs == null || !HisMedicineTypeCFG.HUONG_THAN_IDs.Contains(t.MedicineTypeId))
                    && (HisMedicineTypeCFG.GAY_NGHIEN_IDs == null || !HisMedicineTypeCFG.GAY_NGHIEN_IDs.Contains(t.MedicineTypeId))
                    ).ToList() : null;

                bool gnht = IsNotNullOrEmpty(gayNghienHuongThan);
                bool doc = IsNotNullOrEmpty(thuocDoc);
                bool khac = IsNotNullOrEmpty(thuocKhac) || IsNotNullOrEmpty(data.Materials) || IsNotNullOrEmpty(data.SerialNumbers);

                if (gnht || doc || khac)
                {
                    //Neu ton tai hon 1 loai thi xu ly de tao ra cac y/c ke don
                    if (khac)
                    {
                        InPatientPresSDO sdo = Mapper.Map<InPatientPresSDO>(data);
                        sdo.ServiceReqMaties = null;
                        sdo.ServiceReqMeties = null;
                        sdo.SerialNumbers = data.SerialNumbers;
                        sdo.Materials = data.Materials;
                        sdo.Medicines = thuocKhac;
                        newData.Add(sdo);
                    }
                    if (gnht)
                    {
                        InPatientPresSDO sdo = Mapper.Map<InPatientPresSDO>(data);
                        sdo.Materials = null;
                        sdo.SerialNumbers = null;
                        sdo.ServiceReqMaties = null;
                        sdo.ServiceReqMeties = null;
                        sdo.Medicines = gayNghienHuongThan;
                        sdo.SpecialMedicineType = (long?)HisExpMestCFG.SpecialMedicineType.GN_HT;
                        newData.Add(sdo);
                    }
                    if (doc)
                    {
                        InPatientPresSDO sdo = Mapper.Map<InPatientPresSDO>(data);
                        sdo.Materials = null;
                        sdo.SerialNumbers = null;
                        sdo.ServiceReqMaties = null;
                        sdo.ServiceReqMeties = null;
                        sdo.Medicines = thuocDoc;
                        sdo.SpecialMedicineType = (long?)HisExpMestCFG.SpecialMedicineType.DOC;
                        newData.Add(sdo);
                    }

                    //Gan cac thong tin thuoc/vat tu ke ngoai kho vao ban ghi dau tien
                    newData[0].ServiceReqMaties = data.ServiceReqMaties;
                    newData[0].ServiceReqMeties = data.ServiceReqMeties;

                    return newData;
                }
            }
            return new List<InPatientPresSDO>() { data };
        }
    }
}
