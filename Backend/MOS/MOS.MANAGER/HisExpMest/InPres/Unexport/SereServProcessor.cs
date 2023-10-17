using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.InPres.Unexport
{
    class SereServProcessor : BusinessBase
    {
        private HisSereServUpdate hisSereServUpdate;
        private List<HIS_SERE_SERV> beforeUpdateSereServs = new List<HIS_SERE_SERV>();
        private List<HIS_SERE_SERV> recentDeletedSereServs;

        internal SereServProcessor()
            : base()
        {
            this.hisSereServUpdate = new HisSereServUpdate(param);
        }

        internal SereServProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisSereServUpdate = new HisSereServUpdate(param);
        }

        internal bool Run(HIS_TREATMENT treatment, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            try
            {
                if (!IsNotNull(expMest) || !expMest.SERVICE_REQ_ID.HasValue)
                {
                    return true;
                }

                List<HIS_SERE_SERV> existSereServs = this.GetExistSereServs(expMest, expMestMedicines, expMestMaterials);
                if (IsNotNullOrEmpty(existSereServs))
                {
                    List<HIS_SERE_SERV> toDeletes = existSereServs.Where(o => o.SERVICE_REQ_ID.Value == expMest.SERVICE_REQ_ID.Value).ToList();
                    List<HIS_SERE_SERV> remains = existSereServs.Where(o => o.SERVICE_REQ_ID.Value != expMest.SERVICE_REQ_ID.Value).ToList();

                    //Thuc hien xoa sere_serv
                    this.DeleteSereServ(toDeletes);

                    //Nếu có xóa sere_serv và sau khi xóa vẫn còn dữ liệu thì thực hiện tính toán lại thông tin trong sere_serv
                    if (IsNotNullOrEmpty(toDeletes) && IsNotNullOrEmpty(remains))
                    {
                        this.ProcessReCalc(treatment, remains);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Rollback();
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }

        private void DeleteSereServ(List<HIS_SERE_SERV> sereServs)
        {
            if (IsNotNullOrEmpty(sereServs))
            {
                List<long> sereServIds = sereServs.Select(o => o.ID).ToList();
                string query = DAOWorker.SqlDAO.AddInClause(sereServIds, "UPDATE HIS_SERE_SERV SET IS_DELETE = 1, MEDICINE_ID = NULL, MATERIAL_ID = NULL, BLOOD_ID = NULL WHERE {IN_CLAUSE} ", "ID");
                if (!DAOWorker.SqlDAO.Execute(query))
                {
                    throw new Exception("Xoa sere_serv that bai. Rollback du lieu. Ket thuc nghiep vu");
                }
                this.recentDeletedSereServs = sereServs; //phuc vu rollback
            }
        }

        /// <summary>
        /// Lay ra cac sere_serv tuong ung voi cac 
        /// </summary>
        /// <param name="children"></param>
        /// <param name="expMestMedicines"></param>
        /// <param name="expMestMaterials"></param>
        /// <returns></returns>
        private List<HIS_SERE_SERV> GetExistSereServs(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> presMedicines, List<HIS_EXP_MEST_MATERIAL> presMaterials)
        {
            HisSereServFilterQuery filter = new HisSereServFilterQuery();
            filter.HAS_EXECUTE = true;
            filter.TREATMENT_ID = expMest.TDL_TREATMENT_ID.HasValue ? expMest.TDL_TREATMENT_ID.Value : -1;
            return new HisSereServGet().Get(filter);
        }

        private void ProcessReCalc(HIS_TREATMENT treatment, List<HIS_SERE_SERV> remainSereServs)
        {
            if (IsNotNull(treatment) && IsNotNullOrEmpty(remainSereServs))
            {
                List<HIS_SERE_SERV> changeRecords = new List<HIS_SERE_SERV>();
                List<HIS_SERE_SERV> oldOfChangeRecords = new List<HIS_SERE_SERV>();

                //Xu ly de set thong tin ti le chi tra, doi tuong va lay thong tin thay doi
                if (!new HisSereServUpdateHein(param, treatment, false).Update(remainSereServs, ref changeRecords, ref oldOfChangeRecords))
                {
                    throw new Exception("Rollback du lieu");
                }

                if (IsNotNullOrEmpty(changeRecords))
                {
                    this.beforeUpdateSereServs.AddRange(oldOfChangeRecords);//luu lai phuc vu rollback

                    //tao thread moi de update sere_serv cu~
                    Thread thread = new Thread(new ParameterizedThreadStart(this.ThreadProcessUpdateSereServ));
                    thread.Priority = ThreadPriority.Highest;
                    UpdateSereServThreadData threadData = new UpdateSereServThreadData();
                    threadData.SereServs = changeRecords;
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
                    Inventec.Common.Logging.LogSystem.Error("Huy thuc xuat don ngoai tru: Cap nhat lai ti le BHYT cua sere_serv that bai");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentDeletedSereServs))
                {
                    List<string> sqls = new List<string>();
                    foreach (HIS_SERE_SERV ss in this.recentDeletedSereServs)
                    {
                        string sqlRollback = "";
                        if (ss.MEDICINE_ID.HasValue)
                        {
                            sqlRollback = String.Format("UPDATE HIS_SERE_SERV SET IS_DELETE = 0 , MEDICINE_ID = {0} WHERE ID = {1}", ss.MEDICINE_ID.Value, ss.ID);
                        }
                        else if (ss.MATERIAL_ID.HasValue)
                        {
                            sqlRollback = String.Format("UPDATE HIS_SERE_SERV SET IS_DELETE = 0 , MATERIAL_ID = {0} WHERE ID = {1}", ss.MATERIAL_ID.Value, ss.ID);
                        }
                        if (!String.IsNullOrEmpty(sqlRollback))
                        {
                            sqls.Add(sqlRollback);
                        }
                    }
                    if (IsNotNullOrEmpty(sqls))
                    {
                        if (DAOWorker.SqlDAO.Execute(sqls))
                        {
                            Inventec.Common.Logging.LogSystem.Error("Rollback xoa sere_serv that bai");
                        }
                    }

                    this.recentDeletedSereServs = null; //tranh truong hop goi rollback 2 lan
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
