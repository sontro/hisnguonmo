using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.HisServiceReqMaty;
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
    class HisServiceReqMatyProcessor : BusinessBase
    {
        private HisServiceReqMatyCreate hisServiceReqMatyCreate;

        internal HisServiceReqMatyProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisServiceReqMatyCreate = new HisServiceReqMatyCreate(param);
        }

        internal bool Run(List<long> instructionTimes, List<PresOutStockMatySDO> serviceReqMaties, List<HIS_SERVICE_REQ> inStockServiceReqs, List<HIS_SERVICE_REQ> outStockServiceReqs, ref List<HIS_SERVICE_REQ_MATY> resultData)
        {
            try
            {
                if ((IsNotNullOrEmpty(outStockServiceReqs) || IsNotNullOrEmpty(inStockServiceReqs)) && IsNotNullOrEmpty(serviceReqMaties) && IsNotNullOrEmpty(instructionTimes))
                {
                    List<HIS_SERVICE_REQ_MATY> lst = new List<HIS_SERVICE_REQ_MATY>();
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
                        if (HisExpMestCFG.IS_SPLIT_STAR_MARK
                            && sr.IS_STAR_MARK == Constant.IS_TRUE)
                        {
                            continue;
                        }
                        List<HIS_SERVICE_REQ_MATY> tmp = null;
                        List<PresOutStockMatySDO> sm = null;
                        if (HisServiceReqCFG.MANY_DAYS_PRESCRIPTION_OPTION == HisServiceReqCFG.ManyDaysPrescriptionOption.BY_PRES)
                        {
                            sm = serviceReqMaties;
                        }
                        else
                        {
                            sm = serviceReqMaties.Where(o => o.InstructionTimes.Contains(sr.INTRUCTION_TIME)).ToList();
                        }

                        tmp = sm.Select(o => new HIS_SERVICE_REQ_MATY
                        {
                            AMOUNT = o.Amount,
                            MATERIAL_TYPE_ID = o.MaterialTypeId,
                            MATERIAL_TYPE_NAME = o.MaterialTypeName,
                            NUM_ORDER = o.NumOrder,
                            PRICE = o.Price,
                            SERVICE_REQ_ID = sr.ID,
                            UNIT_NAME = o.UnitName,
                            TUTORIAL = o.Tutorial,
                            TDL_TREATMENT_ID = sr.TREATMENT_ID,
                            PRES_AMOUNT = o.PresAmount,
                            EXCEED_LIMIT_IN_PRES_REASON = o.ExceedLimitInPresReason,
                            EXCEED_LIMIT_IN_DAY_REASON = o.ExceedLimitInDayReason,
                        }).ToList();
                        lst.AddRange(tmp);

                    }

                    if (!this.hisServiceReqMatyCreate.CreateList(lst))
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
            this.hisServiceReqMatyCreate.RollbackData();
        }
    }
}
