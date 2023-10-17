using AutoMapper;
using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Blood
{
    class HisServiceReqBloodPresCreateList : BusinessBase
    {
        private List<HisServiceReqBloodPresCreate> processors = new List<HisServiceReqBloodPresCreate>();

        internal HisServiceReqBloodPresCreateList()
            : base()
        {
        }

        internal HisServiceReqBloodPresCreateList(CommonParam paramCreateList)
            : base(paramCreateList)
        {
        }

        internal bool Run(PatientBloodPresSDO data, ref List<PatientBloodPresResultSDO> resultData)
        {
            bool result = false;
            try
            {
                List<PatientBloodPresSDO> newData = this.SplitPresByBloodType(data);
                if (IsNotNullOrEmpty(newData))
                {
                    resultData = new List<PatientBloodPresResultSDO>();

                    for (int i = 0; i < newData.Count; i++)
                    {
                        PatientBloodPresSDO s = newData[i];
                        PatientBloodPresResultSDO rs = new PatientBloodPresResultSDO();
                        HisServiceReqBloodPresCreate processor = new HisServiceReqBloodPresCreate(param);
                        this.processors.Add(processor);
                        if (processor.Create(s, ref rs))
                        {
                            resultData.Add(rs);
                        }
                        else
                        {
                            throw new Exception("Tao moi danh sach yeu cau mau that bai.Rollback");
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

        private List<PatientBloodPresSDO> SplitPresByBloodType(PatientBloodPresSDO data)
        {
            List<PatientBloodPresSDO> resultData = new List<PatientBloodPresSDO>();
            if (HisServiceReqCFG.IS_SPLIT_BLOOD_PRESCRIPTION_BY_TYPE)
            {
                if (IsNotNull(data) && IsNotNullOrEmpty(data.ExpMestBltyReqs) && data.ExpMestBltyReqs.GroupBy(o => o.BLOOD_TYPE_ID).Count() > 1)
                {
                    // Xu ly tach theo loai va gan vao danh sach yeu cau mau
                    var groups = data.ExpMestBltyReqs.GroupBy(o => o.BLOOD_TYPE_ID);
                    foreach (var g in groups)
                    {
                        Mapper.CreateMap<PatientBloodPresSDO, PatientBloodPresSDO>();
                        PatientBloodPresSDO newSDO = Mapper.Map<PatientBloodPresSDO>(data);
                        newSDO.ExpMestBltyReqs = g.ToList();
                        resultData.Add(newSDO);
                    }
                }
                else
                {
                    resultData.Add(data);
                }
            }
            else
            {
                resultData.Add(data);
            }
            return resultData;
        }

        private void RollbackData()
        {
            try
            {
                if (IsNotNullOrEmpty(this.processors))
                {
                    foreach (HisServiceReqBloodPresCreate p in this.processors)
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
    }
}
