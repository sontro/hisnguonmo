using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisCareDetail;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisCare
{
    class HisCareUpdateWithDhst : BusinessBase
    {
        private HIS_CARE beforeUpdate = null;

        private HisCareDetailCreate hisCareDetailCreate;
        private HisCareDetailUpdate hisCareDetailUpdate;
        private HisDhstCreate hisDhstCreate;

        internal HisCareUpdateWithDhst()
            : base()
        {
            this.Init();
        }

        internal HisCareUpdateWithDhst(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisCareDetailCreate = new HisCareDetailCreate(param);
            this.hisCareDetailUpdate = new HisCareDetailUpdate(param);
            this.hisDhstCreate = new HisDhstCreate(param);
        }

        internal bool Run(HIS_CARE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCareCheck checker = new HisCareCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ValidateData(data);
                HIS_CARE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    List<HIS_CARE_DETAIL> hisCareDetails = data.HIS_CARE_DETAIL != null ? data.HIS_CARE_DETAIL.ToList() : null;
                    List<HIS_DHST> lstDhst = data.HIS_DHST1 != null ? data.HIS_DHST1.ToList() : null;

                    //can set ve null truoc khi update, vi HIS_CARE_DETAIL duoc xu ly de tao moi, chu ko update. Neu ko set ve null se bi loi khi update
                    data.HIS_CARE_DETAIL = null;
                    data.HIS_DHST = null;
                    data.HIS_CARE_SUM = null;
                    data.HIS_DEPARTMENT = null;
                    data.HIS_TRACKING = null;
                    data.HIS_TREATMENT = null;
                    data.HIS_AWARENESS = null;
                    data.HIS_DHST1 = null;

                    if (String.IsNullOrWhiteSpace(data.EXECUTE_LOGINNAME))
                    {
                        data.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        data.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    }
                    if (data.EXECUTE_TIME.HasValue)
                    {
                        data.EXECUTE_DATE = data.EXECUTE_TIME.Value - (data.EXECUTE_TIME.Value % 1000000);
                    }

                    if (!DAOWorker.HisCareDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCare_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCare that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdate = raw;
                    List<string> sqls = new List<string>();

                    this.ProcessCareDetail(hisCareDetails, data, ref sqls);
                    this.ProcessDhst(lstDhst, data, ref sqls);

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Sqls: " + sqls.ToString());
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void ProcessCareDetail(List<HIS_CARE_DETAIL> lstCareDetail, HIS_CARE care, ref List<string> sqls)
        {
            List<HIS_CARE_DETAIL> lstOld = new HisCareDetailGet().GetByCareId(care.ID);
            if (IsNotNullOrEmpty(lstCareDetail) || IsNotNullOrEmpty(lstOld))
            {
                List<HIS_CARE_DETAIL> creates = new List<HIS_CARE_DETAIL>();
                List<HIS_CARE_DETAIL> updates = new List<HIS_CARE_DETAIL>();
                List<HIS_CARE_DETAIL> befores = new List<HIS_CARE_DETAIL>();
                List<HIS_CARE_DETAIL> deletes = new List<HIS_CARE_DETAIL>();

                if (IsNotNullOrEmpty(lstCareDetail))
                {
                    foreach (HIS_CARE_DETAIL detail in lstCareDetail)
                    {
                        detail.CARE_ID = care.ID;
                        HIS_CARE_DETAIL old = lstOld != null ? lstOld.FirstOrDefault(o => o.CARE_TYPE_ID == detail.CARE_TYPE_ID) : null;

                        if (old != null)
                        {
                            detail.ID = old.ID;
                            updates.Add(detail);
                            befores.Add(old);
                        }
                        else
                        {
                            creates.Add(detail);
                        }
                    }
                }

                deletes = lstOld != null ? lstOld.Where(o => updates == null || !updates.Any(a => a.ID == o.ID)).ToList() : null;

                if (IsNotNullOrEmpty(creates) && !this.hisCareDetailCreate.CreateList(creates))
                {
                    throw new Exception("hisCareDetailCreate. Ket thuc nghiep vu");
                }

                if (IsNotNullOrEmpty(updates) && !this.hisCareDetailUpdate.UpdateList(updates, befores))
                {
                    throw new Exception("hisCareDetailUpdate. Ket thuc nghiep vu");
                }

                if (IsNotNullOrEmpty(deletes))
                {
                    sqls.Add(DAOWorker.SqlDAO.AddInClause(deletes.Select(s => s.ID).ToList(), "DELETE HIS_CARE_DETAIL WHERE %IN_CLAUSE% ", "ID"));
                }
            }
        }

        private void ProcessDhst(List<HIS_DHST> lstDhst, HIS_CARE care, ref List<string> sqls)
        {
            List<HIS_DHST> lstOld = new HisDhstGet().GetByCareId(care.ID);
            if (IsNotNullOrEmpty(lstDhst) || IsNotNullOrEmpty(lstOld))
            {
                lstDhst.ForEach(o =>
                    {
                        o.TREATMENT_ID = care.TREATMENT_ID;
                        o.CARE_ID = care.ID;
                        o.ID = 0;
                        o.EXECUTE_DEPARTMENT_ID = care.EXECUTE_DEPARTMENT_ID;
                        o.EXECUTE_LOGINNAME = care.EXECUTE_LOGINNAME;
                        o.EXECUTE_ROOM_ID = care.EXECUTE_ROOM_ID;
                        o.EXECUTE_USERNAME = care.EXECUTE_USERNAME;
                    });

                if (IsNotNullOrEmpty(lstDhst) && !this.hisDhstCreate.CreateList(lstDhst))
                {
                    throw new Exception("hisDhstCreate. Ket thuc nghiep vu");
                }

                if (IsNotNullOrEmpty(lstOld))
                {
                    sqls.Add(DAOWorker.SqlDAO.AddInClause(lstOld.Select(s => s.ID).ToList(), "DELETE HIS_DHST WHERE %IN_CLAUSE% ", "ID"));
                }
            }
        }

        private void Rollback()
        {
            try
            {
                this.hisDhstCreate.RollbackData();
                this.hisCareDetailUpdate.RollbackData();
                this.hisCareDetailCreate.RollbackData();
                if (this.beforeUpdate != null)
                {
                    if (!DAOWorker.HisCareDAO.Update(this.beforeUpdate))
                    {
                        LogSystem.Warn("Rollback du lieu HisCare that bai, can kiem tra lai." + LogUtil.TraceData("HisCare", this.beforeUpdate));
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
