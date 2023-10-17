using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServReha
{
    partial class HisSereServRehaCreate : BusinessBase
    {
        private List<HIS_SERE_SERV_REHA> recentHisSereServRehas = new List<HIS_SERE_SERV_REHA>();

        internal HisSereServRehaCreate()
            : base()
        {

        }

        internal HisSereServRehaCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HisSereServRehaSDO data, ref List<HIS_SERE_SERV_REHA> resultData)
        {
            bool result = false;
            try
            {
                if (data != null && IsNotNullOrEmpty(data.RehaTrainTypeIds) && data.SereServId > 0)
                {
                    List<long> rehaTrainTypeIds = data.RehaTrainTypeIds.Distinct().ToList();

                    List<HIS_SERE_SERV_REHA> exists = new HisSereServRehaGet().GetBySereServId(data.SereServId);
                    List<HIS_SERE_SERV_REHA> listToDeletes = null;
                    List<HIS_SERE_SERV_REHA> listToInserts = null;
                    if (exists != null)
                    {
                        listToDeletes = exists.Where(o => !rehaTrainTypeIds.Contains(o.REHA_TRAIN_TYPE_ID)).ToList();
                        listToInserts = rehaTrainTypeIds
                            .Where(o => !exists.Where(t => t.REHA_TRAIN_TYPE_ID == o).Any())
                            .Select(o => new HIS_SERE_SERV_REHA
                            {
                                REHA_TRAIN_TYPE_ID = o,
                                SERE_SERV_ID = data.SereServId
                            }).ToList();
                    }
                    else
                    {
                        listToInserts = rehaTrainTypeIds
                            .Select(o => new HIS_SERE_SERV_REHA
                            {
                                REHA_TRAIN_TYPE_ID = o,
                                SERE_SERV_ID = data.SereServId
                            }).ToList();
                    }
                    if (IsNotNullOrEmpty(listToDeletes))
                    {
                        if (!new HisSereServRehaTruncate(param).TruncateList(listToDeletes))
                        {
                            throw new Exception("Xu ly that bai. Ket thuc nghiep vu");
                        }
                    }
                    if (IsNotNullOrEmpty(listToInserts))
                    {
                        if (!new HisSereServRehaCreate(param).CreateList(listToInserts))
                        {
                            throw new Exception("Xu ly that bai. Ket thuc nghiep vu");
                        }
                    }
                    resultData = new List<HIS_SERE_SERV_REHA>();
                    if (IsNotNullOrEmpty(exists))
                    {
                        resultData.AddRange(exists);
                    }
                    if (IsNotNullOrEmpty(listToInserts))
                    {
                        resultData.AddRange(listToInserts);
                    }
                    if (IsNotNullOrEmpty(listToDeletes))
                    {
                        foreach (HIS_SERE_SERV_REHA t in listToDeletes)
                        {
                            resultData.Remove(t);
                        }
                    }
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

        internal bool Create(HIS_SERE_SERV_REHA data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServRehaCheck checker = new HisSereServRehaCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsNotAdded(data);
                if (valid)
                {
                    if (!DAOWorker.HisSereServRehaDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServReha_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServReha that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSereServRehas.Add(data);
                    result = true;
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

        internal bool CreateList(List<HIS_SERE_SERV_REHA> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServRehaCheck checker = new HisSereServRehaCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSereServRehaDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServReha_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServReha that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSereServRehas.AddRange(listData);
                    result = true;
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

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisSereServRehas))
            {
                if (!new HisSereServRehaTruncate(param).TruncateList(this.recentHisSereServRehas))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServReha that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSereServRehas", this.recentHisSereServRehas));
                }
            }
        }
    }
}
