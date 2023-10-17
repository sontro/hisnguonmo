using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ.Create
{
    class HisSereServCreateBatchUsingThread : BusinessBase
    {
        private HisSereServUpdate hisSereServUpdate;
        private HisSereServCreateSql hisSereServCreateSql;

        private List<HIS_SERE_SERV> beforeUpdateSereServs = new List<HIS_SERE_SERV>();

        internal HisSereServCreateBatchUsingThread(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisSereServCreateSql = new HisSereServCreateSql(param);
        }

        internal void Run(List<HIS_TREATMENT> treatments, List<HIS_SERVICE_REQ> serviceReqs, List<HIS_SERE_SERV> existedSereServs, List<HIS_SERE_SERV> newSereServs)
        {
            if (IsNotNullOrEmpty(newSereServs) && IsNotNullOrEmpty(treatments) && IsNotNullOrEmpty(serviceReqs))
            {
                long maxId = IsNotNullOrEmpty(existedSereServs) ? existedSereServs.Max(o => o.ID) : 0;
                long maxExistId = maxId;

                //Tao ID "fake" de dinh danh cac sere_serv chua co trong DB
                newSereServs.ForEach(o => o.ID = ++maxId);

                List<HIS_SERE_SERV> changeRecords = new List<HIS_SERE_SERV>();
                List<HIS_SERE_SERV> oldOfChangeRecords = new List<HIS_SERE_SERV>();

                //Tinh toan lai theo tung treatment
                foreach (HIS_TREATMENT treatment in treatments)
                {
                    List<HIS_SERE_SERV> news = newSereServs.Where(o => o.TDL_TREATMENT_ID == treatment.ID).ToList();
                    List<HIS_SERE_SERV> exists = existedSereServs != null ? existedSereServs.Where(o => o.TDL_TREATMENT_ID == treatment.ID).ToList() : null;
                    this.ProcessCalc(treatment, news, exists, ref changeRecords, ref oldOfChangeRecords);
                }
                
                List<HIS_SERE_SERV> toUpdates = IsNotNullOrEmpty(changeRecords) ? changeRecords.Where(o => o.ID <= maxExistId).ToList() : null;

                
                if (!this.hisSereServCreateSql.Run(newSereServs, serviceReqs))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }

                if (IsNotNullOrEmpty(toUpdates))
                {
                    List<HIS_SERE_SERV> olds = oldOfChangeRecords.Where(o => o.ID <= maxExistId).ToList();
                    this.beforeUpdateSereServs.AddRange(olds);//luu lai phuc vu rollback

                    //tao thread moi de update sere_serv cu~
                    Thread thread = new Thread(new ParameterizedThreadStart(this.ThreadProcessUpdateSereServ));
                    thread.Priority = ThreadPriority.Highest;
                    UpdateSereServThreadData threadData = new UpdateSereServThreadData();
                    threadData.SereServs = toUpdates;
                    thread.Start(threadData);
                }
            }
        }

        private void ProcessCalc(HIS_TREATMENT treatment, List<HIS_SERE_SERV> news, List<HIS_SERE_SERV> exists, ref List<HIS_SERE_SERV> changeRecords, ref List<HIS_SERE_SERV> oldOfChangeRecords)
        {
            List<HIS_SERE_SERV> toUpdateData = new List<HIS_SERE_SERV>();
            toUpdateData.AddRange(news);
            if (IsNotNullOrEmpty(exists))
            {
                toUpdateData.AddRange(exists);
            }

            List<HIS_SERE_SERV> changes = null;
            List<HIS_SERE_SERV> oldOfChanges = null;
            //Xu ly de set thong tin ti le chi tra, doi tuong va lay thong tin thay doi
            if (!new HisSereServUpdateHein(param, treatment, false).Update(exists, toUpdateData, ref changes, ref oldOfChanges))
            {
                throw new Exception("Rollback du lieu");
            }

            if (IsNotNullOrEmpty(changes))
            {
                changeRecords.AddRange(changes);
                oldOfChangeRecords.AddRange(oldOfChanges);
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
            this.hisSereServCreateSql.Rollback();
            if (IsNotNullOrEmpty(this.beforeUpdateSereServs))
            {
                this.beforeUpdateSereServs = null;

                //tao thread moi de update sere_serv cu~
                Thread thread = new Thread(new ParameterizedThreadStart(this.ThreadProcessUpdateSereServ));
                thread.Priority = ThreadPriority.Highest;
                UpdateSereServThreadData threadData = new UpdateSereServThreadData();
                threadData.SereServs = this.beforeUpdateSereServs;
                thread.Start(threadData);
            }
        }
    }
}
