using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisKskContract.Import
{
    class HisKskContractImport : BusinessBase
    {
        private List<HisKskContractImportDetail> importDetails = new List<HisKskContractImportDetail>();

        internal HisKskContractImport()
            : base()
        {

        }

        internal HisKskContractImport(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisKskContractSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<PrepareData> prepareData = new List<PrepareData>();
                WorkPlaceSDO workPlace = null;
                HisKskContractImportPrepare preparer = new HisKskContractImportPrepare(param);
                HisKskContractCheck commonChecker = new HisKskContractCheck(param);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && preparer.Prepare(data, workPlace, ref prepareData);
                if (valid)
                {
                    Mapper.CreateMap<CommonParam, CommonParam>();
                    foreach (PrepareData pd in prepareData)
                    {
                        string desc = null;
                        CommonParam commonParam = Mapper.Map<CommonParam>(param);
                        HisKskContractImportDetail importDetail = new HisKskContractImportDetail(commonParam);
                        this.importDetails.Add(importDetail);
                        if (!importDetail.Run(pd, workPlace, data.Loginname, data.Username, ref desc))
                        {
                            throw new Exception("Rollback du lieu");
                        }
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void Rollback()
        {
            if (IsNotNullOrEmpty(this.importDetails))
            {
                foreach(HisKskContractImportDetail d in this.importDetails)
                {
                    d.Rollback();
                }
            }
        }
    }
}
