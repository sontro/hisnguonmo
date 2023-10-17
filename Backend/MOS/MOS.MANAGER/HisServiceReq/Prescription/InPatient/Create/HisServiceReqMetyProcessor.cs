using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.HisServiceReqMety;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient.Create
{
    class HisServiceReqMetyProcessor : BusinessBase
    {
        private HisServiceReqMetyCreate hisServiceReqMetyCreate;

        internal HisServiceReqMetyProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisServiceReqMetyCreate = new HisServiceReqMetyCreate(param);
        }

        internal bool Run(List<long> instructionTimes, List<PresOutStockMetySDO> serviceReqMeties, List<HIS_SERVICE_REQ> inStockServiceReqs, List<HIS_SERVICE_REQ> outStockServiceReqs, ref List<HIS_SERVICE_REQ_METY> resultData)
        {
            try
            {
                if ((IsNotNullOrEmpty(outStockServiceReqs) || IsNotNullOrEmpty(inStockServiceReqs)) && IsNotNullOrEmpty(serviceReqMeties) && IsNotNullOrEmpty(instructionTimes))
                {
                    List<HIS_SERVICE_REQ_METY> lst = new List<HIS_SERVICE_REQ_METY>();
                    List<HIS_SERVICE_REQ> serviceReqs = new List<HIS_SERVICE_REQ>();
                    if (IsNotNullOrEmpty(outStockServiceReqs))
                    {
                        serviceReqs.AddRange(outStockServiceReqs);
                    }
                    else
                    {
                        foreach (long t in instructionTimes)
                        {
                            //Trong truong hop ke don nhieu ngay va moi ngay lai co chon nhieu kho
                            //--> chi lay don dieu tri dau tien cua tuong ung voi ngay y lenh day de gan thuoc ngoai kho vao
                            HIS_SERVICE_REQ serviceReq = inStockServiceReqs.Where(o => o.INTRUCTION_TIME == t).FirstOrDefault();
                            serviceReqs.Add(serviceReq);
                        }
                    }
                    foreach (HIS_SERVICE_REQ sr in serviceReqs)
                    {
                        List<HIS_SERVICE_REQ_METY> tmp = new List<HIS_SERVICE_REQ_METY>();
                        List<PresOutStockMetySDO> sm = null;

                        if (HisServiceReqCFG.MANY_DAYS_PRESCRIPTION_OPTION == HisServiceReqCFG.ManyDaysPrescriptionOption.BY_PRES)
                        {
                            if (!HisExpMestCFG.IS_SPLIT_STAR_MARK)
                            {
                                sm = serviceReqMeties;
                            }
                            else
                            {
                                if (sr.IS_STAR_MARK == Constant.IS_TRUE)
                                {
                                    sm = serviceReqMeties
                                        .Where(o => o.MedicineTypeId.HasValue
                                            && HisMedicineTypeCFG.STAR_IDs != null
                                            && HisMedicineTypeCFG.STAR_IDs.Contains(o.MedicineTypeId.Value)).ToList();
                                }
                                else
                                {
                                    sm = serviceReqMeties
                                        .Where(o => !o.MedicineTypeId.HasValue
                                            || HisMedicineTypeCFG.STAR_IDs == null
                                            || !HisMedicineTypeCFG.STAR_IDs.Contains(o.MedicineTypeId.Value)).ToList();
                                }
                            }
                        }
                        else
                        {
                            if (!HisExpMestCFG.IS_SPLIT_STAR_MARK)
                            {
                                sm = serviceReqMeties.Where(o => o.InstructionTimes.Contains(sr.INTRUCTION_TIME)).ToList();
                            }
                            else
                            {
                                if (sr.IS_STAR_MARK == Constant.IS_TRUE)
                                {
                                    sm = serviceReqMeties.Where(o => o.InstructionTimes.Contains(sr.INTRUCTION_TIME)
                                            && o.MedicineTypeId.HasValue
                                            && HisMedicineTypeCFG.STAR_IDs != null
                                            && HisMedicineTypeCFG.STAR_IDs.Contains(o.MedicineTypeId.Value)).ToList();
                                }
                                else
                                {
                                    sm = serviceReqMeties
                                        .Where(o => o.InstructionTimes.Contains(sr.INTRUCTION_TIME))
                                        .Where(o => !o.MedicineTypeId.HasValue
                                            || HisMedicineTypeCFG.STAR_IDs == null
                                            || !HisMedicineTypeCFG.STAR_IDs.Contains(o.MedicineTypeId.Value)).ToList();
                                }
                            }
                        }

                        foreach (var sdo in sm)
                        {
                            HIS_SERVICE_REQ_METY srMety = new HIS_SERVICE_REQ_METY();
                            srMety.AMOUNT = sdo.Amount;
                            srMety.MEDICINE_TYPE_ID = sdo.MedicineTypeId;
                            srMety.MEDICINE_TYPE_NAME = sdo.MedicineTypeName;
                            srMety.NUM_ORDER = sdo.NumOrder;
                            srMety.PRICE = sdo.Price;
                            srMety.SERVICE_REQ_ID = sr.ID;
                            srMety.UNIT_NAME = sdo.UnitName;
                            srMety.MEDICINE_USE_FORM_ID = sdo.MedicineUseFormId;
                            srMety.SPEED = sdo.Speed;
                            srMety.USE_TIME_TO = sdo.UseTimeTo;
                            srMety.TUTORIAL = sdo.Tutorial;
                            srMety.NOON = sdo.Noon;
                            srMety.AFTERNOON = sdo.Afternoon;
                            srMety.EVENING = sdo.Evening;
                            srMety.MORNING = sdo.Morning;
                            srMety.HTU_ID = sdo.HtuId;
                            srMety.TDL_TREATMENT_ID = sr.TREATMENT_ID;
                            srMety.PRES_AMOUNT = sdo.PresAmount;
                            srMety.PREVIOUS_USING_COUNT = sdo.PreviousUsingCount;
                            srMety.EXCEED_LIMIT_IN_PRES_REASON = sdo.ExceedLimitInPresReason;
                            srMety.EXCEED_LIMIT_IN_DAY_REASON = sdo.ExceedLimitInDayReason;
                            srMety.ODD_PRES_REASON = sdo.OddPresReason;
                            if (IsNotNullOrEmpty(sdo.MedicineInfoSdos))
                            {
                                foreach (var item in sdo.MedicineInfoSdos)
                                {
                                    if (!item.IsNoPrescription && item.IntructionTime == sr.INTRUCTION_TIME)
                                    {
                                        srMety.OVER_KIDNEY_REASON = item.OverKidneyReason;
                                        srMety.OVER_RESULT_TEST_REASON = item.OverResultTestReason;
                                    }
                                }
                            }
                            tmp.Add(srMety);
                        }
                        lst.AddRange(tmp);
                    }

                    if (!this.hisServiceReqMetyCreate.CreateList(lst))
                    {
                        return false;
                    }
                    resultData = lst;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return true;
        }

        internal void Rollback()
        {
            this.hisServiceReqMetyCreate.RollbackData();
        }
    }
}
