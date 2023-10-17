using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMediStock;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Test
{
    /// <summary>
    /// Xu ly phieu xuat hoa chat chay xet nghiem
    /// </summary>
    class HisExpMestTestCreate : BusinessBase
    {
        private List<HIS_EXP_MEST_MATERIAL> recentExpMestMaterials;
        private HIS_EXP_MEST recentExpMest;

        private HisExpMestProcessor hisExpMestProcessor;
        private MaterialProcessor materialProcessor;

        internal HisExpMestTestCreate()
            : base()
        {
            this.Init();
        }

        internal HisExpMestTestCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestProcessor = new HisExpMestProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
        }

        internal bool Run(ExpMestTestSDO data, ref ExpMestTestResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                
                List<HIS_SERE_SERV_TEIN> sereServTeins = null;
                HisExpMestTestCreateCheck checker = new HisExpMestTestCreateCheck(param);

                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && checker.IsValidData(data, ref sereServTeins);
                valid = valid && checker.IsAllowMediStock(workPlace, data.MediStockId);
                
                if (valid)
                {
                    List<string> sqls = new List<string>();

                    if (!this.hisExpMestProcessor.Run(data, ref this.recentExpMest))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.materialProcessor.Run(data.Materials, this.recentExpMest, ref this.recentExpMestMaterials, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    this.UpdateSereServTein(this.recentExpMest, sereServTeins, ref sqls);

                    //Can execute sql o cuoi de tranh rollback du lieu
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback");
                    }

                    this.PassResult(ref resultData);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                resultData = null;
                result = false;
            }
            return result;
        }

        private void UpdateSereServTein(HIS_EXP_MEST expMest, List<HIS_SERE_SERV_TEIN> sereServTeins, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(sereServTeins) && expMest != null)
            {
                List<long> ids = sereServTeins.Select(o => o.ID).ToList();
                string sql = DAOWorker.SqlDAO.AddInClause(ids, "UPDATE HIS_SERE_SERV_TEIN SET EXP_MEST_ID = {0} WHERE %IN_CLAUSE%", "ID");
                sql = string.Format(sql, expMest.ID);
                sqls.Add(sql);
            }
        }

        private void PassResult(ref ExpMestTestResultSDO resultData)
        {
            resultData = new ExpMestTestResultSDO();
            resultData.ExpMest = this.recentExpMest != null ? new HisExpMestGet().GetViewById(this.recentExpMest.ID) : null;

            if (IsNotNullOrEmpty(this.recentExpMestMaterials))
            {
                List<long> expMestMaterialIds = this.recentExpMestMaterials.Select(o => o.ID).ToList();
                resultData.ExpMestMaterials = new HisExpMestMaterialGet().GetViewByIds(expMestMaterialIds);
            }
        }


        private void RollbackData()
        {
            this.materialProcessor.Rollback();
            this.hisExpMestProcessor.Rollback();
        }
    }
}