using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineType;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Create;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReq.Common;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Unexport
{
    class SereServProcessor : BusinessBase
    {
        private HisSereServUpdate hisSereServUpdate;
        private List<HIS_SERE_SERV> beforeUpdateSereServs = new List<HIS_SERE_SERV>();
        private List<long> recentDeletedSereServIds;

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

        internal bool Run(HIS_EXP_MEST expMest, HIS_TREATMENT treatment, List<HIS_SERE_SERV> existSereServs)
        {
            try
            {
                //Chỉ đơn máu thì hủy thực xuất mới thực hiện xóa sere_serv
                if (IsNotNullOrEmpty(existSereServs) && expMest.SERVICE_REQ_ID.HasValue && expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM)
                {
                    List<HIS_SERE_SERV> toDeletes = existSereServs
                        .Where(o => o.SERVICE_REQ_ID.Value == expMest.SERVICE_REQ_ID.Value
                            && o.BLOOD_ID.HasValue //chi xoa sere_serv voi don mau, thuoc/vat tu van giu nguyen
                        ).ToList();
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
                //this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
        }

        private void DeleteSereServ(List<HIS_SERE_SERV> sereServs)
        {
            if (IsNotNullOrEmpty(sereServs))
            {
                List<long> sereServIds = sereServs.Select(o => o.ID).ToList();
                string query = DAOWorker.SqlDAO.AddInClause(sereServIds, "UPDATE HIS_SERE_SERV SET IS_DELETE = 1, MEDICINE_ID = NULL, MATERIAL_ID = NULL, BLOOD_ID = NULL WHERE %IN_CLAUSE% ", "ID");
                
                if (!DAOWorker.SqlDAO.Execute(query))
                {
                    throw new Exception("Xoa sere_serv that bai. Rollback du lieu. Ket thuc nghiep vu");
                }

                this.recentDeletedSereServIds = sereServIds; //phuc vu rollback
            }
        }

        private void ProcessReCalc(HIS_TREATMENT treatment, List<HIS_SERE_SERV> remainSereServs)
        {
            if (treatment != null && IsNotNullOrEmpty(remainSereServs))
            {
                List<HIS_SERE_SERV> changes = null;
                List<HIS_SERE_SERV> oldOfChanges = null;
                //Xu ly de set thong tin ti le chi tra, doi tuong va lay thong tin thay doi
                if (!new HisSereServUpdateHein(param, treatment, false).Update(remainSereServs, ref changes, ref oldOfChanges))
                {
                    throw new Exception("Rollback du lieu");
                }

                if (IsNotNullOrEmpty(changes))
                {
                    this.beforeUpdateSereServs.AddRange(oldOfChanges);//luu lai phuc vu rollback

                    //tao thread moi de update sere_serv cu~
                    Thread thread = new Thread(new ParameterizedThreadStart(this.ThreadProcessUpdateSereServ));
                    thread.Priority = ThreadPriority.Highest;
                    UpdateSereServThreadData threadData = new UpdateSereServThreadData();
                    threadData.SereServs = changes;
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
                    LogSystem.Error("Huy thuc xuat phieu linh: Cap nhat lai ti le BHYT cua sere_serv that bai");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentDeletedSereServIds))
                {
                    string query = DAOWorker.SqlDAO.AddInClause(this.recentDeletedSereServIds, "UPDATE HIS_SERE_SERV SET IS_DELETE = 0 WHERE %IN_CLAUSE% ", "ID");
                    if (!DAOWorker.SqlDAO.Execute(query))
                    {
                        LogSystem.Error("Rollback xoa sere_serv that bai");
                    }
                    this.recentDeletedSereServIds = null; //tranh truong hop goi rollback 2 lan
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
