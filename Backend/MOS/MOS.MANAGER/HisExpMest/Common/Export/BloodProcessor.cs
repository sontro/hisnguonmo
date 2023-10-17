using AutoMapper;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestBlood;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using Inventec.Core;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using System.Threading;
using MOS.MANAGER.HisServiceReq.Common;

namespace MOS.MANAGER.HisExpMest.Common.Export
{
    partial class BloodProcessor : BusinessBase
    {
        private List<long> recentBloodIds;
        private long recentMediStockId;

        private HisExpMestBloodUpdate hisExpMestBloodUpdate;
        private HisSereServCreate hisSereServCreate;
        private HisSereServUpdate hisSereServUpdate;
        private HisSereServUpdateHein hisSereServUpdateHein;

        internal BloodProcessor()
            : base()
        {
            this.Init();
        }

        internal BloodProcessor(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestBloodUpdate = new HisExpMestBloodUpdate(param);
            this.hisSereServCreate = new HisSereServCreate(param);
            this.hisSereServUpdate = new HisSereServUpdate(param);
        }

        internal bool Run(List<HIS_EXP_MEST_BLOOD> hisExpMestBloods, HIS_EXP_MEST expMest, HIS_TREATMENT treatment, string loginName, string userName, long? expTime)
        {
            try
            {
                this.recentMediStockId = expMest.MEDI_STOCK_ID;
                this.ProcessBlood(hisExpMestBloods, expMest.MEDI_STOCK_ID);
                this.ProcessExpMestBlood(hisExpMestBloods, loginName, userName, expTime);
                this.ProcessSereServ(hisExpMestBloods, expMest, treatment);
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Cap nhat his_blood_bean
        /// </summary>
        /// <param name="hisExpMestBloods"></param>
        internal void ProcessBlood(List<HIS_EXP_MEST_BLOOD> hisExpMestBloods, long mediStockId)
        {
            if (IsNotNullOrEmpty(hisExpMestBloods))
            {
                List<long> bloodIds = hisExpMestBloods.Select(o => o.BLOOD_ID).ToList();

                //Lay ra danh sach blood tuong ung voi phieu xuat
                List<HIS_BLOOD> bloods = new HisBloodGet().GetByIds(bloodIds);

                if (IsNotNullOrEmpty(bloods))
                {
                    //Kiem tra xem co blood_bean nao ko thuoc kho hoac chua khoa hay khong
                    List<HIS_BLOOD> invalidBloods = bloods.Where(o => o.MEDI_STOCK_ID != mediStockId || o.IS_ACTIVE == MOS.UTILITY.Constant.IS_TRUE).ToList();

                    if (IsNotNullOrEmpty(invalidBloods))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception("Ton tai blood chua duoc khoa hoac ko thuoc kho can xuat. " + LogUtil.TraceData("invalidBloods", invalidBloods));
                    }

                    string query = DAOWorker.SqlDAO.AddInClause(bloodIds, "UPDATE HIS_BLOOD SET IS_ACTIVE = 1, MEDI_STOCK_ID = NULL WHERE %IN_CLAUSE% ", "ID");
                    
                    if (!DAOWorker.SqlDAO.Execute(query))
                    {
                        throw new Exception("Update de thuc xuat blood that bai. Rollback du lieu. Ket thuc nghiep vu");
                    }

                    this.recentBloodIds = bloodIds; //phuc vu rollback
                }
            }
        }

        private void ProcessExpMestBlood(List<HIS_EXP_MEST_BLOOD> hisExpMestBloods, string loginName, string userName, long? expTime)
        {
            if (IsNotNullOrEmpty(hisExpMestBloods))
            {
                Mapper.CreateMap<HIS_EXP_MEST_BLOOD, HIS_EXP_MEST_BLOOD>();
                List<HIS_EXP_MEST_BLOOD> befores = Mapper.Map<List<HIS_EXP_MEST_BLOOD>>(hisExpMestBloods);

                hisExpMestBloods.ForEach(o =>
                {
                    o.IS_EXPORT = MOS.UTILITY.Constant.IS_TRUE;
                    o.EXP_LOGINNAME = loginName;
                    o.EXP_USERNAME = userName;
                    o.EXP_TIME = expTime;
                });

                if (!this.hisExpMestBloodUpdate.UpdateList(hisExpMestBloods, befores))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        /// <summary>
        /// Trong truong hop don mau thi sere_serv duoc tao luc thuc xuat
        /// </summary>
        private void ProcessSereServ(List<HIS_EXP_MEST_BLOOD> hisExpMestBloods, HIS_EXP_MEST expMest, HIS_TREATMENT treatment)
        {
            if (IsNotNullOrEmpty(hisExpMestBloods) && expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM)
            {
                if (!expMest.SERVICE_REQ_ID.HasValue)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Don mau khong co SERVICE_REQ_ID" + LogUtil.TraceData("expMest", expMest));
                }
                HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(expMest.SERVICE_REQ_ID.Value);

                HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);

                List<HIS_SERE_SERV> existedSereServs = new HisSereServGet().GetByTreatmentId(expMest.TDL_TREATMENT_ID.Value);
                long maxExistedSereServId = IsNotNullOrEmpty(existedSereServs) ? existedSereServs.Max(o => o.ID) : 0;
                List<HIS_SERE_SERV> newSereServs = new List<HIS_SERE_SERV>();
                List<HIS_SERE_SERV> toUpdateData = new List<HIS_SERE_SERV>();
                long countSereServId = maxExistedSereServId;
                foreach (HIS_EXP_MEST_BLOOD blood in hisExpMestBloods)
                {
                    HIS_BLOOD_TYPE bloodType = HisBloodTypeCFG.DATA.Where(o => o.ID == blood.TDL_BLOOD_TYPE_ID).FirstOrDefault();
                    HIS_SERE_SERV ss = new HIS_SERE_SERV();
                    ss.SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                    ss.SERVICE_ID = bloodType.SERVICE_ID;
                    ss.AMOUNT = 1; //so luong luon la 1
                    ss.PATIENT_TYPE_ID = blood.PATIENT_TYPE_ID.Value;
                    ss.PRICE = blood.PRICE.HasValue ? blood.PRICE.Value : 0;
                    ss.VAT_RATIO = blood.VAT_RATIO.HasValue ? blood.VAT_RATIO.Value : 0;
                    ss.ORIGINAL_PRICE = ss.PRICE;
                    ss.PRIMARY_PRICE = ss.PRICE;
                    ss.BLOOD_ID = blood.BLOOD_ID;
                    ss.PARENT_ID = blood.SERE_SERV_PARENT_ID;
                    ss.IS_OUT_PARENT_FEE = blood.IS_OUT_PARENT_FEE;
                    ss.ID = ++countSereServId;
                    HisSereServUtil.SetTdl(ss, serviceReq);
                    priceAdder.AddPriceForNonService(ss, serviceReq.INTRUCTION_TIME, serviceReq.ICD_CODE, serviceReq.ICD_SUB_CODE);
                    newSereServs.Add(ss);
                }
                if (IsNotNullOrEmpty(newSereServs))
                {
                    toUpdateData.AddRange(newSereServs);
                }
                if (IsNotNullOrEmpty(existedSereServs))
                {
                    toUpdateData.AddRange(existedSereServs);
                }

                List<HIS_SERE_SERV> changeRecords = null;
                List<HIS_SERE_SERV> oldOfChangeRecords = null;
                this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, false);
                //Xu ly de set thong tin ti le chi tra, doi tuong va lay thong tin thay doi
                if (!this.hisSereServUpdateHein.Update(existedSereServs, toUpdateData, ref changeRecords, ref oldOfChangeRecords))
                {
                    throw new Exception("Rollback du lieu");
                }

                //Cap nhat lai cac sere_serv bi thay doi thong tin

                List<HIS_SERE_SERV> toUpdates = IsNotNullOrEmpty(changeRecords) ? changeRecords.Where(o => o.ID <= maxExistedSereServId).ToList() : null;

                if (!this.hisSereServCreate.CreateList(newSereServs, serviceReq, false))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }

                if (IsNotNullOrEmpty(toUpdates))
                {
                    //tao thread moi de update sere_serv cu~
                    Thread thread = new Thread(new ParameterizedThreadStart(this.ThreadProcessUpdateSereServ));
                    thread.Priority = ThreadPriority.Highest;
                    UpdateSereServThreadData threadData = new UpdateSereServThreadData();
                    threadData.SereServs = toUpdates;
                    thread.Start(threadData);
                }
            }
        }

        private void ThreadProcessUpdateSereServ(object threadData)
        {
            try
            {
                UpdateSereServThreadData td = (UpdateSereServThreadData)threadData;
                List<HIS_SERE_SERV> sereServs = td.SereServs;

                if (!this.hisSereServUpdate.UpdateRaw(sereServs))
                {
                    LogSystem.Error("Cap nhat lai ti le BHYT cua sere_serv that bai");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal void Rollback()
        {
            this.hisSereServCreate.RollbackData();
            if (this.hisSereServUpdateHein != null) this.hisSereServUpdateHein.RollbackData();

            if (IsNotNullOrEmpty(this.recentBloodIds))
            {
                string query = DAOWorker.SqlDAO.AddInClause(this.recentBloodIds, "UPDATE HIS_BLOOD SET IS_ACTIVE = 0, MEDI_STOCK_ID = :param1 WHERE %IN_CLAUSE% ", "ID");
                if (!DAOWorker.SqlDAO.Execute(query, this.recentMediStockId))
                {
                    LogSystem.Warn("Rollback blood_bean that bai");
                }
                this.recentBloodIds = null; //tranh truong hop goi rollback 2 lan
            }

            this.hisExpMestBloodUpdate.RollbackData();
        }
    }
}
