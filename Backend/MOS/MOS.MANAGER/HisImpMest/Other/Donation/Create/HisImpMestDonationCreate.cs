using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisBloodGiver;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Other.Donation.Create
{
    partial class HisImpMestDonationCreate : BusinessBase
    {
        private HIS_IMP_MEST recentHisImpMest;
        private List<HIS_BLOOD_GIVER> recentBloodGivers;
        private List<HIS_BLOOD> recentBloods;

        private HisImpMestCreate hisImpMestCreate;
        private HisBloodGiverCreate hisBloodGiverCreate;
        private BloodProcessor bloodProcessor;
        private HisImpMestAutoProcess hisImpMestAutoProcess;

        internal HisImpMestDonationCreate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestDonationCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestCreate = new HisImpMestCreate(param);
            this.bloodProcessor = new BloodProcessor(param);
            this.hisImpMestAutoProcess = new HisImpMestAutoProcess(param);
            this.hisBloodGiverCreate = new HisBloodGiverCreate(param);
        }

        internal bool Create(HisImpMestDonationSDO data, ref HisImpMestDonationSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                V_HIS_MEDI_STOCK hisMediStock = null;
                HisImpMestCheck checker = new HisImpMestCheck(param);
                HisImpMestDonationChecker donationChecker = new HisImpMestDonationChecker(param);
                valid = valid && donationChecker.VerifyRequireField(data);
                valid = valid && donationChecker.VerifyBloodGiver(data.DonationDetail);
                valid = valid && checker.IsValidMediStock(data.ImpMest, ref hisMediStock);
                if (valid)
                {
                    this.ProcessHisImpMest(data);

                    this.recentBloodGivers = data.DonationDetail.Select(s => s.BloodGiver).ToList();

                    this.recentBloodGivers.ForEach(o => o.IMP_MEST_ID = this.recentHisImpMest.ID);

                    if (!this.hisBloodGiverCreate.CreateList(this.recentBloodGivers))
                    {
                        throw new Exception("Tao HIS_BLOOD_GIVER that bai. Ket thuc nghiep vu. Rollback du lieu");
                    }

                    List<HIS_BLOOD> listBloods = new List<HIS_BLOOD>();
                    foreach (var item in data.DonationDetail)
                    {
                        HIS_BLOOD_GIVER giver = this.recentBloodGivers.FirstOrDefault(o => o.GIVE_CODE == item.BloodGiver.GIVE_CODE);
                        if (IsNotNull(giver))
                        {
                            item.Bloods.ForEach(o => o.BLOOD_GIVE_ID = giver.ID);
                        }

                        listBloods.AddRange(item.Bloods);
                    }

                    //Xu ly mau
                    if (!this.bloodProcessor.Run(listBloods, this.recentHisImpMest, ref this.recentBloods))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    new EventLogGenerator(EventLog.Enum.HisImpMest_TaoPhieuNhap).ImpMestCode(this.recentHisImpMest.IMP_MEST_CODE).Run();

                    this.ProcessAuto();

                    this.PassResult(ref resultData);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                result = false;
                resultData = null;
            }
            return result;
        }

        private void ProcessHisImpMest(HisImpMestDonationSDO data)
        {
            Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
            HIS_IMP_MEST hisImpMest = Mapper.Map<HIS_IMP_MEST>(data.ImpMest);
            hisImpMest.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__HM;

            if (!this.hisImpMestCreate.Create(hisImpMest))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisImpMest = hisImpMest;
        }

        private void ProcessAuto()
        {
            try
            {
                this.hisImpMestAutoProcess.Run(this.recentHisImpMest);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Truyen ket qua thong qua bien "data"
        /// </summary>
        /// <param name="data"></param>
        private void PassResult(ref HisImpMestDonationSDO data)
        {
            //truy van lai de lay du lieu IMP_MEST_CODE tra ve client, phuc vu in
            data = new HisImpMestDonationSDO();
            data.ImpMest = new HisImpMestGet().GetById(this.recentHisImpMest.ID);
            data.DonationDetail = new List<DonationDetailSDO>();
            if (IsNotNullOrEmpty(this.recentBloodGivers))
            {
                foreach (var item in this.recentBloodGivers)
                {
                    DonationDetailSDO sdo = new DonationDetailSDO();
                    sdo.BloodGiver = item;
                    if (IsNotNullOrEmpty(this.recentBloods))
                    {
                        sdo.Bloods = this.recentBloods.Where(o => o.BLOOD_GIVE_ID == item.ID).ToList();
                    }

                    data.DonationDetail.Add(sdo);
                }
            }
        }

        internal void RollbackData()
        {
            this.bloodProcessor.Rollback();
            this.hisBloodGiverCreate.RollbackData();
            this.hisImpMestCreate.RollbackData();
        }
    }
}
