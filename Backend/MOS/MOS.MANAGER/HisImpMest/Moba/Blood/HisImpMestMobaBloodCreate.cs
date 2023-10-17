using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisImpMestBlood;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Moba.Blood
{
    class HisImpMestMobaBloodCreate : BusinessBase
    {
        private List<HIS_IMP_MEST_BLOOD> recentHisImpMestBloods;

        private HIS_EXP_MEST expMest;
        private HIS_IMP_MEST recentHisImpMest;

        private HisImpMestCreate hisImpMestCreate;
        private BloodProcessor bloodProcessor;
        private SereServProcessor sereServProcessor;

        internal HisImpMestMobaBloodCreate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestMobaBloodCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestCreate = new HisImpMestCreate(param);
            this.bloodProcessor = new BloodProcessor(param);
            this.sereServProcessor = new SereServProcessor(param);
        }

        internal bool Create(HisImpMestMobaBloodSDO data, ref HisImpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_IMP_MEST impMest = new HIS_IMP_MEST();
                HisImpMestMobaCheck checker = new HisImpMestMobaCheck(param);
                valid = valid && IsNotNullOrEmpty(data.BloodIds);
                valid = valid && checker.VerifyExpMestId(impMest, data.ExpMestId, null, ref this.expMest);
                valid = valid && checker.CheckValidMobaBlood(impMest, this.expMest);
                valid = valid && checker.CheckValidRequestRoom(impMest, this.expMest, data.RequestRoomId);
                valid = valid && checker.VerifyServiceReq(this.expMest);
                if (valid)
                {
                    this.ProcessHisImpMest(data, impMest);

                    if (!this.bloodProcessor.Run(data, this.recentHisImpMest, ref this.recentHisImpMestBloods))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (!this.sereServProcessor.Run(this.recentHisImpMestBloods, this.expMest))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    this.PassResult(ref resultData);

                    new EventLogGenerator(EventLog.Enum.HisImpMest_TaoPhieuNhapThuHoi).ImpMestCode(this.recentHisImpMest.IMP_MEST_CODE).ExpMestCode(this.expMest.EXP_MEST_CODE).Run();

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessHisImpMest(HisImpMestMobaBloodSDO data, HIS_IMP_MEST impMest)
        {
            impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
            impMest.TDL_MOBA_EXP_MEST_CODE = this.expMest.EXP_MEST_CODE;
            impMest.DESCRIPTION = data.Description;
            impMest.TRACKING_ID = data.TrackingId;

            HisImpMestUtil.SetTdl(impMest, expMest);
            if (!this.hisImpMestCreate.Create(impMest))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisImpMest = impMest;
        }

        private void PassResult(ref HisImpMestResultSDO resultSDO)
        {
            resultSDO = new HisImpMestResultSDO();
            resultSDO.ImpMest = new HisImpMestGet().GetViewById(this.recentHisImpMest.ID);
            if (IsNotNullOrEmpty(this.recentHisImpMestBloods))
            {
                resultSDO.ImpBloods = new HisImpMestBloodGet().GetViewByImpMestId(this.recentHisImpMest.ID);
            }
        }

        internal void RollbackData()
        {
            this.sereServProcessor.Rollback();
            this.bloodProcessor.Rollback();
            this.hisImpMestCreate.RollbackData();
        }
    }
}
