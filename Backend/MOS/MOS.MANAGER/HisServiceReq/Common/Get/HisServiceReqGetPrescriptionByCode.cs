using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMaterialType;
using MOS.MANAGER.HisMedicineType;
using MOS.MANAGER.HisMedicineTypeAcin;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisServiceReqMaty;
using MOS.MANAGER.HisServiceReqMety;
using MOS.MANAGER.HisTreatment;
using MOS.TDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq
{
    partial class HisServiceReqGet : GetBase
    {
        internal PrescriptionTDO GetPrescriptionByCode(string ServiceReqCode)
        {
            PrescriptionTDO result = null;
            try
            {
                V_HIS_SERVICE_REQ_2 vServiceReq = new HisServiceReqGet().GetView2ByServiceReqCode(ServiceReqCode);

                if (this.IsValidServiceReqCode(vServiceReq))
                {
                    HIS_TREATMENT treatment = new HisTreatmentGet().GetById(vServiceReq.TREATMENT_ID);

                    List<HIS_EXP_MEST_MEDICINE> expMestMedicines = new HisExpMestMedicineGet().GetByServiceReqId(vServiceReq.ID);

                    List<HIS_EXP_MEST_MATERIAL> expMestMaterials = new HisExpMestMaterialGet().GetByServiceReqId(vServiceReq.ID);

                    List<HIS_SERVICE_REQ_METY> srMety = new HisServiceReqMetyGet().GetByServiceReqId(vServiceReq.ID);

                    List<HIS_SERVICE_REQ_MATY> srMaty = new HisServiceReqMatyGet().GetByServiceReqId(vServiceReq.ID);

                    List<long> medicineTypeIds = new List<long>();
                    List<V_HIS_MEDICINE_TYPE_ACIN> vMedicineTypeAcin = null;
                    List<V_HIS_MEDICINE_TYPE> medicineType = null;
                    if (IsNotNullOrEmpty(expMestMedicines))
                    {
                        medicineTypeIds.AddRange(expMestMedicines.Select(o => o.TDL_MEDICINE_TYPE_ID ?? 0).ToList());
                    }
                    if (IsNotNullOrEmpty(srMety))
                    {
                        medicineTypeIds.AddRange(srMety.Select(o => o.MEDICINE_TYPE_ID ?? 0).ToList());
                    }
                    if (IsNotNullOrEmpty(medicineTypeIds))
                    {
                        medicineTypeIds = medicineTypeIds.Distinct().ToList();
                        vMedicineTypeAcin = new HisMedicineTypeAcinGet().GetViewByMedicineTypeIds(medicineTypeIds);
                        medicineType = new HisMedicineTypeGet().GetViewByIds(medicineTypeIds);
                    }

                    List<long> materialTypeIds = new List<long>();
                    List<V_HIS_MATERIAL_TYPE> materialType = null;
                    if (IsNotNullOrEmpty(expMestMaterials))
                    {
                        materialTypeIds.AddRange(expMestMaterials.Select(o => o.TDL_MATERIAL_TYPE_ID ?? 0).ToList());
                    }
                    if (IsNotNullOrEmpty(srMaty))
                    {
                        materialTypeIds.AddRange(srMaty.Select(o => o.MATERIAL_TYPE_ID ?? 0).ToList());
                    }
                    if (IsNotNullOrEmpty(materialTypeIds))
                    {
                        materialTypeIds = materialTypeIds.Distinct().ToList();
                        materialType = new HisMaterialTypeGet().GetViewByIds(materialTypeIds);
                    }

                    PrescriptionTDO tdo = new PrescriptionTDO();
                    tdo.PatientCode = vServiceReq.TDL_PATIENT_CODE;
                    tdo.TreatmentCode = vServiceReq.TDL_TREATMENT_CODE;
                    tdo.ServiceReqCode = vServiceReq.SERVICE_REQ_CODE;
                    tdo.IntructionTime = (Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(vServiceReq.INTRUCTION_TIME) ?? DateTime.Now).ToString("yyyy-MM-dd hh:mm:ss");
                    tdo.CreateTime = (Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(vServiceReq.CREATE_TIME ?? 0) ?? DateTime.Now).ToString("yyyy-MM-dd hh:mm:ss");
                    tdo.PatientName = vServiceReq.TDL_PATIENT_NAME;
                    if (treatment != null)
                    {
                        if (treatment.TDL_PATIENT_IS_HAS_NOT_DAY_DOB == Constant.IS_TRUE)
                        {
                            tdo.PatientDob = (Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(treatment.TDL_PATIENT_DOB) ?? DateTime.Now).ToString("yyyy");
                        }
                        else
                        {
                            tdo.PatientDob = (Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(treatment.TDL_PATIENT_DOB) ?? DateTime.Now).ToString("yyyy-MM-dd");
                        }
                        tdo.GenderName = treatment.TDL_PATIENT_GENDER_NAME;
                        HIS_PATIENT_TYPE patientType = new HisPatientTypeGet().GetById(treatment.TDL_PATIENT_TYPE_ID ?? 0);
                        tdo.PatientTypeName = patientType.PATIENT_TYPE_NAME;
                        tdo.PatientAddress = treatment.TDL_PATIENT_ADDRESS;
                        tdo.PatientPhone = treatment.TDL_PATIENT_PHONE != null ? treatment.TDL_PATIENT_PHONE : treatment.TDL_PATIENT_MOBILE;
                        tdo.RelativePhone = treatment.TDL_PATIENT_RELATIVE_PHONE != null ? treatment.TDL_PATIENT_RELATIVE_PHONE : treatment.TDL_PATIENT_RELATIVE_MOBILE;
                        tdo.IcdCode = treatment.ICD_CODE;
                        tdo.IcdName = treatment.ICD_NAME;
                        tdo.IcdSubCode = treatment.ICD_SUB_CODE;
                        tdo.IcdText = treatment.ICD_TEXT;
                    }

                    tdo.RequestRoomCode = vServiceReq.REQUEST_ROOM_CODE;
                    tdo.RequestRoomName = vServiceReq.REQUEST_ROOM_NAME;
                    tdo.RequestLoginname = vServiceReq.REQUEST_LOGINNAME;
                    tdo.RequestUsername = vServiceReq.REQUEST_USERNAME;

                    HIS_EMPLOYEE employee = HisEmployeeCFG.DATA.FirstOrDefault(o => o.LOGINNAME == vServiceReq.REQUEST_LOGINNAME);

                    tdo.RequestDisploma = employee != null ? employee.DIPLOMA : "";

                    tdo.PrescriptionDetail = new List<PrescriptionDetailTDO>();

                    if (IsNotNullOrEmpty(expMestMaterials))
                    {
                        foreach (var mate in expMestMaterials)
                        {
                            PrescriptionDetailTDO detail = new PrescriptionDetailTDO();
                            V_HIS_MATERIAL_TYPE vmateType = IsNotNullOrEmpty(materialType) ? materialType.FirstOrDefault(o => o.ID == mate.TDL_MATERIAL_TYPE_ID) : null;
                            if (vmateType != null)
                            {
                                detail.ServiceCode = vmateType.MATERIAL_TYPE_CODE;
                                detail.ServiceName = vmateType.MATERIAL_TYPE_NAME;
                                detail.HeinServiceBhytName = vmateType.HEIN_SERVICE_BHYT_NAME;
                                detail.ServiceUnitName = vmateType.SERVICE_UNIT_NAME;
                            }

                            detail.Tutorial = mate.TUTORIAL;
                            detail.Amount = mate.AMOUNT;
                            detail.numOrder = mate.NUM_ORDER ?? 0;

                            tdo.PrescriptionDetail.Add(detail);
                        }
                    }

                    if (IsNotNullOrEmpty(srMaty))
                    {
                        foreach (var maty in srMaty)
                        {
                            PrescriptionDetailTDO detailmaty = new PrescriptionDetailTDO();
                            V_HIS_MATERIAL_TYPE vmateType = IsNotNullOrEmpty(materialType) ? materialType.FirstOrDefault(o => o.ID == maty.MATERIAL_TYPE_ID) : null;
                            if (vmateType != null)
                            {
                                detailmaty.ServiceCode = vmateType.MATERIAL_TYPE_CODE;
                                detailmaty.ServiceName = vmateType.MATERIAL_TYPE_NAME;
                                detailmaty.HeinServiceBhytName = vmateType.HEIN_SERVICE_BHYT_NAME;
                                detailmaty.ServiceUnitName = vmateType.SERVICE_UNIT_NAME;
                            }
                            else
                            {
                                detailmaty.ServiceName = maty.MATERIAL_TYPE_NAME;
                                detailmaty.ServiceUnitName = maty.UNIT_NAME;
                            }

                            detailmaty.Tutorial = maty.TUTORIAL;
                            detailmaty.Amount = maty.AMOUNT;
                            detailmaty.numOrder = maty.NUM_ORDER ?? 0;

                            tdo.PrescriptionDetail.Add(detailmaty);
                        }
                    }

                    if (IsNotNullOrEmpty(expMestMedicines))
                    {
                        foreach (var expMestMedi in expMestMedicines)
                        {
                            PrescriptionDetailTDO detailExpMestMedi = new PrescriptionDetailTDO();
                            V_HIS_MEDICINE_TYPE vmediType = IsNotNullOrEmpty(medicineType) ? medicineType.FirstOrDefault(o => o.ID == expMestMedi.TDL_MEDICINE_TYPE_ID) : null;
                            if (vmediType != null)
                            {
                                detailExpMestMedi.ServiceCode = vmediType.MEDICINE_TYPE_CODE;
                                detailExpMestMedi.ServiceName = vmediType.MEDICINE_TYPE_NAME;
                                detailExpMestMedi.ActiveIngrBhytName = vmediType.ACTIVE_INGR_BHYT_NAME;
                                detailExpMestMedi.HeinServiceBhytName = vmediType.HEIN_SERVICE_BHYT_NAME;
                                detailExpMestMedi.ServiceUnitName = vmediType.SERVICE_UNIT_NAME;
                            }

                            List<V_HIS_MEDICINE_TYPE_ACIN> activeIngredients = IsNotNullOrEmpty(vMedicineTypeAcin) ? vMedicineTypeAcin.Where(o => o.MEDICINE_TYPE_ID == expMestMedi.TDL_MEDICINE_TYPE_ID).ToList() : null;
                            if (IsNotNullOrEmpty(activeIngredients))
                            {
                                detailExpMestMedi.ActiveIngredient = string.Join(" + ", activeIngredients.OrderBy(p => p.ACTIVE_INGREDIENT_NAME).Select(o => o.ACTIVE_INGREDIENT_NAME).ToList());
                            }

                            detailExpMestMedi.Amount = expMestMedi.AMOUNT;
                            if (expMestMedi.USE_TIME_TO.HasValue)
                            {
                                DateTime dtUseTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(vServiceReq.USE_TIME ?? vServiceReq.INTRUCTION_TIME).Value;
                                DateTime dtUseTimeTo = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(expMestMedi.USE_TIME_TO.Value).Value;
                                TimeSpan ts = dtUseTimeTo.Date - dtUseTime.Date;
                                detailExpMestMedi.UseDays = ts.Days + 1;
                            }
                            detailExpMestMedi.numOrder = expMestMedi.NUM_ORDER ?? 0;
                            detailExpMestMedi.Tutorial = expMestMedi.TUTORIAL;

                            tdo.PrescriptionDetail.Add(detailExpMestMedi);
                        }
                    }

                    if (IsNotNullOrEmpty(srMety))
                    {
                        foreach (var svMety in srMety)
                        {
                            PrescriptionDetailTDO detailMestMety = new PrescriptionDetailTDO();
                            V_HIS_MEDICINE_TYPE vmediType = IsNotNullOrEmpty(medicineType) ? medicineType.FirstOrDefault(o => o.ID == svMety.MEDICINE_TYPE_ID) : null;

                            if (vmediType != null)
                            {
                                detailMestMety.ServiceCode = vmediType.MEDICINE_TYPE_CODE;
                                detailMestMety.ServiceName = vmediType.MEDICINE_TYPE_NAME;
                                detailMestMety.ActiveIngrBhytName = vmediType.ACTIVE_INGR_BHYT_NAME;
                                detailMestMety.HeinServiceBhytName = vmediType.HEIN_SERVICE_BHYT_NAME;
                                detailMestMety.ServiceUnitName = vmediType.SERVICE_UNIT_NAME;
                            }
                            else
                            {
                                detailMestMety.ServiceName = svMety.MEDICINE_TYPE_NAME;
                                detailMestMety.ServiceUnitName = svMety.UNIT_NAME;
                            }

                            List<V_HIS_MEDICINE_TYPE_ACIN> activeIngredients = IsNotNullOrEmpty(vMedicineTypeAcin) ? vMedicineTypeAcin.Where(o => o.MEDICINE_TYPE_ID == svMety.MEDICINE_TYPE_ID).ToList() : null;
                            if (IsNotNullOrEmpty(activeIngredients))
                            {
                                detailMestMety.ActiveIngredient = string.Join(" + ", activeIngredients.OrderBy(p => p.ACTIVE_INGREDIENT_NAME).Select(o => o.ACTIVE_INGREDIENT_NAME).ToList());
                            }

                            detailMestMety.Amount = svMety.AMOUNT;
                            if (svMety.USE_TIME_TO.HasValue)
                            {
                                DateTime dtUseTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(vServiceReq.USE_TIME ?? vServiceReq.INTRUCTION_TIME).Value;
                                DateTime dtUseTimeTo = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(svMety.USE_TIME_TO.Value).Value;
                                TimeSpan ts = dtUseTimeTo.Date - dtUseTime.Date;
                                detailMestMety.UseDays = ts.Days + 1;
                            }
                            detailMestMety.numOrder = svMety.NUM_ORDER ?? 0;
                            detailMestMety.ServiceUnitName = svMety.UNIT_NAME;
                            detailMestMety.Tutorial = svMety.TUTORIAL;

                            tdo.PrescriptionDetail.Add(detailMestMety);
                        }
                    }

                    tdo.PrescriptionDetail = tdo.PrescriptionDetail.OrderBy(o => o.numOrder).ToList();

                    result = tdo;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }

        internal bool IsValidServiceReqCode(V_HIS_SERVICE_REQ_2 serviceReq)
        {
            bool valid = true;
            try
            {
                if (serviceReq == null || !HisServiceReqTypeCFG.PRESCRIPTION_TYPE_IDs.Contains(serviceReq.SERVICE_REQ_TYPE_ID)
                    || serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONM)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                    return false;
                }
            }
            catch (Exception ex)
            {
                valid = false;
                LogSystem.Error(ex);
            }

            return valid;
        }
    }
}
