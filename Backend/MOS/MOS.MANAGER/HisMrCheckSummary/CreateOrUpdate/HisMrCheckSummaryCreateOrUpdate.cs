using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMrChecklist;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.SDO;
using AutoMapper;

namespace MOS.MANAGER.HisMrCheckSummary
{
    class HisMrCheckSummaryCreateOrUpdate : BusinessBase
    {
        private List<HIS_MR_CHECK_SUMMARY> createdMrCheckSummarys = new List<HIS_MR_CHECK_SUMMARY>();
        private List<HIS_MR_CHECK_SUMMARY> beforeMrCheckSummarys = new List<HIS_MR_CHECK_SUMMARY>();
        private List<HIS_MR_CHECKLIST> MrChecklistUpdates = new List<HIS_MR_CHECKLIST>();
        private List<HIS_MR_CHECKLIST> MrChecklistCreates = new List<HIS_MR_CHECKLIST>();
        private List<HIS_MR_CHECKLIST> MrChecklistDeletes = new List<HIS_MR_CHECKLIST>();

        internal HisMrCheckSummaryCreateOrUpdate()
            : base()
        {
        }

        internal HisMrCheckSummaryCreateOrUpdate(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(MrCheckSummarySDO data, ref MrCheckSummarySDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMrCheckSummaryCheck checker = new HisMrCheckSummaryCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (IsGreaterThanZero(data.HisMrCheckSummary.ID))
                    {
                        HIS_MR_CHECK_SUMMARY raw = null;
                        valid = valid && checker.VerifyId(data.HisMrCheckSummary.ID, ref raw);

                        List<HIS_MR_CHECKLIST> checklistUpdate = new List<HIS_MR_CHECKLIST>();
                        List<HIS_MR_CHECKLIST> checklistCreate = new List<HIS_MR_CHECKLIST>();
                        List<HIS_MR_CHECKLIST> checklistToUpdate = new List<HIS_MR_CHECKLIST>();
                        List<HIS_MR_CHECKLIST> checklistToDelete = new List<HIS_MR_CHECKLIST>();

                        checklistCreate = data.HisMrChecklists.Where(o => o.ID == 0).ToList();
                        checklistToUpdate = data.HisMrChecklists.Where(o => o.ID != 0).ToList();
                        checklistToDelete = new HisMrChecklistGet().GetByMrCheckSummaryId(data.HisMrCheckSummary.ID);

                        List<long> existsIds = checklistToUpdate.Select(s => s.ID).Distinct().ToList();
                        if (existsIds.Exists(o => o == 0))
                        {
                            existsIds.Remove(0);
                        }

                        if (IsNotNullOrEmpty(existsIds))
                        {
                            HisMrChecklistCheck checklistChecker = new HisMrChecklistCheck(param);
                            valid = valid && checklistChecker.VerifyIds(existsIds, checklistUpdate);
                        }

                        if (valid)
                        {
                            if (IsNotNullOrEmpty(existsIds))
                            {
                                checklistToDelete = checklistToDelete.Where(o => !existsIds.Contains(o.ID)).ToList();
                            }

                            if (IsNotNullOrEmpty(checklistToDelete))
                            {
                                MrChecklistDeletes.AddRange(checklistToDelete); // phuc vu rollback
                                if (!new HisMrChecklistTruncate(param).TruncateList(checklistToDelete))
                                {
                                    throw new Exception("Xoa thong tin HisMrChecklist cu theo MrCheckSummaryId that bai");
                                }
                            }

                            // Cap nhat thong tin summary
                            if (!DAOWorker.HisMrCheckSummaryDAO.Update(data.HisMrCheckSummary))
                            {
                                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrCheckSummary_CapNhatThatBai);
                                throw new Exception("Cap nhat thong tin HisMrCheckSummary that bai." + LogUtil.TraceData("data", data));
                            }

                            beforeMrCheckSummarys.Add(raw);

                            if (IsNotNullOrEmpty(checklistCreate))
                            {
                                checklistCreate.ForEach(o => o.MR_CHECK_SUMMARY_ID = data.HisMrCheckSummary.ID);
                                if (!DAOWorker.HisMrChecklistDAO.CreateList(checklistCreate))
                                {
                                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrChecklist_ThemMoiThatBai);
                                    throw new Exception("Them moi thong tin HisMrChecklist that bai." + LogUtil.TraceData("listData", checklistCreate));
                                }

                                MrChecklistCreates.AddRange(checklistCreate);
                            }

                            if (IsNotNullOrEmpty(checklistToUpdate))
                            {
                                checklistToUpdate.ForEach(o => o.MR_CHECK_SUMMARY_ID = data.HisMrCheckSummary.ID);
                                if (!DAOWorker.HisMrChecklistDAO.UpdateList(checklistToUpdate))
                                {
                                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrChecklist_CapNhatThatBai);
                                    throw new Exception("Cap nhat thong tin HisMrChecklist that bai." + LogUtil.TraceData("listData", checklistToUpdate));
                                }

                                MrChecklistUpdates.AddRange(checklistUpdate);
                            }

                            result = true;
                        }
                    }
                    else
                    {
                        data.HisMrChecklists.ForEach(o => o.ID = 0);

                        Mapper.CreateMap<HIS_MR_CHECK_SUMMARY, HIS_MR_CHECK_SUMMARY>();
                        HIS_MR_CHECK_SUMMARY createData = Mapper.Map<HIS_MR_CHECK_SUMMARY>(data.HisMrCheckSummary);

                        createData.HIS_MR_CHECKLIST = data.HisMrChecklists;

                        if (!DAOWorker.HisMrCheckSummaryDAO.Create(createData))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrCheckSummary_ThemMoiThatBai);
                            throw new Exception("Them moi thong tin HisMrCheckSummary that bai." + LogUtil.TraceData("data", data));
                        }

                        this.createdMrCheckSummarys.Add(createData);

                        result = true;
                    }

                    this.PassResult(ref resultData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void PassResult(ref MrCheckSummarySDO data)
        {
            data = new MrCheckSummarySDO();

            long id = 0;
            if (IsNotNullOrEmpty(this.createdMrCheckSummarys))
            {
                id = this.createdMrCheckSummarys.First().ID;
            }
            else if (IsNotNullOrEmpty(this.beforeMrCheckSummarys))
            {
                id = this.beforeMrCheckSummarys.First().ID;
            }

            //truy van lai de lay du lieu IMP_MEST_CODE tra ve client, phuc vu in
            data.HisMrCheckSummary = new HisMrCheckSummaryGet().GetById(id);
            data.HisMrChecklists = new HisMrChecklistGet().GetByMrCheckSummaryId(id);
        }

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.createdMrCheckSummarys))
            {
                if (!DAOWorker.HisMrCheckSummaryDAO.TruncateList(this.createdMrCheckSummarys))
                {
                    LogSystem.Warn("Rollback du lieu HisMrCheckSummary that bai, can kiem tra lai." + LogUtil.TraceData("createdMrCheckSummarys", this.createdMrCheckSummarys));
                }
                this.createdMrCheckSummarys = null;
            }

            if (IsNotNullOrEmpty(this.beforeMrCheckSummarys))
            {
                if (!DAOWorker.HisMrCheckSummaryDAO.UpdateList(this.beforeMrCheckSummarys))
                {
                    LogSystem.Warn("Rollback du lieu HisMrCheckSummary that bai, can kiem tra lai." + LogUtil.TraceData("beforeMrCheckSummarys", this.beforeMrCheckSummarys));
                }
                this.beforeMrCheckSummarys = null;
            }

            if (IsNotNullOrEmpty(this.MrChecklistUpdates))
            {
                if (!DAOWorker.HisMrChecklistDAO.UpdateList(this.MrChecklistUpdates))
                {
                    LogSystem.Warn("Rollback du lieu HisMrChecklist that bai, can kiem tra lai." + LogUtil.TraceData("MrChecklistUpdates", this.MrChecklistUpdates));
                }
                this.MrChecklistUpdates = null;
            }

            if (IsNotNullOrEmpty(this.MrChecklistCreates))
            {
                if (!DAOWorker.HisMrChecklistDAO.TruncateList(this.MrChecklistCreates))
                {
                    LogSystem.Warn("Rollback du lieu HisMrChecklist that bai, can kiem tra lai." + LogUtil.TraceData("MrChecklistCreates", this.MrChecklistCreates));
                }
                this.MrChecklistCreates = null;
            }

            if (IsNotNullOrEmpty(this.MrChecklistDeletes))
            {
                if (!DAOWorker.HisMrChecklistDAO.CreateList(this.MrChecklistDeletes))
                {
                    LogSystem.Warn("Rollback du lieu HisMrChecklist that bai, can kiem tra lai." + LogUtil.TraceData("MrChecklistDeletes", this.MrChecklistDeletes));
                }
                this.MrChecklistDeletes = null;
            }
        }
    }
}