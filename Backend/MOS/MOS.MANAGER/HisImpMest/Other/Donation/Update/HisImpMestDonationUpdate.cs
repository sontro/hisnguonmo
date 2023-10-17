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

namespace MOS.MANAGER.HisImpMest.Other.Donation.Update
{
    class HisImpMestDonationUpdate : BusinessBase
    {
        private HisImpMestUpdate hisImpMestUpdate;
        private BloodProcessor bloodProcessor;
        private HisImpMestAutoProcess hisImpMestAutoProcess;
        private HisBloodGiverCreate hisBloodGiverCreate;
        private HisBloodGiverUpdate hisBloodGiverUpdate;
        private HisBloodGiverTruncate hisBloodGiverTruncate;

        private HIS_IMP_MEST recentHisImpMest;
        private List<HIS_BLOOD_GIVER> recentBloodGivers;
        private List<HIS_BLOOD> recentBloods;

        internal HisImpMestDonationUpdate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestDonationUpdate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestUpdate = new HisImpMestUpdate(param);
            this.bloodProcessor = new BloodProcessor(param);
            this.hisImpMestAutoProcess = new HisImpMestAutoProcess(param);
            this.hisBloodGiverCreate = new HisBloodGiverCreate(param);
            this.hisBloodGiverUpdate = new HisBloodGiverUpdate(param);
            this.hisBloodGiverTruncate = new HisBloodGiverTruncate(param);
        }

        internal bool Update(HisImpMestDonationSDO data, ref HisImpMestDonationSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                V_HIS_MEDI_STOCK hisMediStock = null;
                HisImpMestCheck checker = new HisImpMestCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.ImpMest);
                valid = valid && IsNotNullOrEmpty(data.DonationDetail);
                valid = valid && checker.IsValidMediStock(data.ImpMest, ref hisMediStock);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    HisBloodGiverFilterQuery bloodGiverFilter = new HisBloodGiverFilterQuery();
                    bloodGiverFilter.IMP_MEST_ID = data.ImpMest.ID;
                    List<HIS_BLOOD_GIVER> oldData = new HisBloodGiverGet().Get(bloodGiverFilter);

                    List<HIS_BLOOD_GIVER> createData = data.DonationDetail.Select(s => s.BloodGiver).Where(o => o.ID == 0).ToList();
                    List<HIS_BLOOD_GIVER> updaterData = data.DonationDetail.Select(s => s.BloodGiver).Where(o => o.ID > 0).ToList();
                    List<HIS_BLOOD_GIVER> deletesData = new List<HIS_BLOOD_GIVER>();
                    if (IsNotNullOrEmpty(updaterData))
                    {
                        deletesData = oldData.Where(o => !updaterData.Exists(e => e.ID == o.ID)).ToList();
                    }
                    else
                    {
                        deletesData = oldData;
                    }

                    //
                    if (IsNotNullOrEmpty(updaterData) && updaterData.Exists(o => o.IMP_MEST_ID != data.ImpMest.ID))
                    {
                        throw new Exception("Thong tin nguoi nhan khong dung. Ket thuc nghiep vu. Rollback du lieu");
                    }

                    this.ProcessHisImpMest(data);
                    this.recentBloodGivers = new List<HIS_BLOOD_GIVER>();

                    createData.ForEach(o => o.IMP_MEST_ID = this.recentHisImpMest.ID);
                    if (IsNotNullOrEmpty(createData) && !this.hisBloodGiverCreate.CreateList(createData))
                    {
                        throw new Exception("Tao HIS_BLOOD_GIVER that bai. Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(updaterData) && !this.hisBloodGiverUpdate.UpdateList(updaterData))
                    {
                        throw new Exception("Sua HIS_BLOOD_GIVER that bai. Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(createData))
                    {
                        this.recentBloodGivers.AddRange(createData);
                    }

                    if (IsNotNullOrEmpty(updaterData))
                    {
                        this.recentBloodGivers.AddRange(updaterData);
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

                    if (!this.bloodProcessor.Run(this.recentHisImpMest, listBloods, ref this.recentBloods, ref sqls))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    //xóa sau khi xóa máu
                    if (IsNotNullOrEmpty(deletesData))
                    {
                        string sql = DAOWorker.SqlDAO.AddInClause(deletesData.Select(s => s.ID).ToList(), "DELETE HIS_BLOOD_GIVER WHERE %IN_CLAUSE% ", "ID");
                        sqls.Add(sql);
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Sql Execute. sql: " + sqls.ToString());
                    }

                    new EventLogGenerator(EventLog.Enum.HisImpMest_SuaPhieuNhap).ImpMestCode(this.recentHisImpMest.IMP_MEST_CODE).Run();

                    this.ProcessAuto();

                    this.PassResult(ref resultData);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                resultData = null;
                result = false;
            }
            return result;
        }

        private void ProcessHisImpMest(HisImpMestDonationSDO data)
        {
            if (!this.hisImpMestUpdate.Update(data.ImpMest))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisImpMest = data.ImpMest;
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
            data = new HisImpMestDonationSDO();
            //truy van lai de lay du lieu IMP_MEST_CODE tra ve client, phuc vu in
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

        private void Rollback()
        {
            try
            {
                this.bloodProcessor.Rollback();
                this.hisBloodGiverCreate.RollbackData();
                this.hisBloodGiverUpdate.RollbackData();
                this.hisImpMestUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
