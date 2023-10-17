using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMaterialTypeMap;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMaterialType
{
    class HisMaterialTypeUpdateMap : BusinessBase
    {
        private HisMaterialTypeMapCreate hisMaterialTypeMapCreate;

        internal HisMaterialTypeUpdateMap()
            : base()
        {
            this.hisMaterialTypeMapCreate = new HisMaterialTypeMapCreate(param);
        }

        internal HisMaterialTypeUpdateMap(CommonParam param)
            : base(param)
        {
            this.hisMaterialTypeMapCreate = new HisMaterialTypeMapCreate(param);
        }

        internal bool Run(HisMaterialTypeUpdateMapSDO data,ref HIS_MATERIAL_TYPE resultData)
        {
            bool result = false;
            try
            {
                HIS_MATERIAL_TYPE raw = null;
                long? currentMaterialTypeMapId = null;
                List<HIS_MATERIAL_TYPE> news = new List<HIS_MATERIAL_TYPE>();
                bool valid = true;
                HisMaterialTypeCheck checker = new HisMaterialTypeCheck(param);

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.MaterialTypeId, ref raw);
                valid = valid && checker.VerifyIds(data.MapMaterialTypeIds, news);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    List<HIS_MATERIAL_TYPE> olds = null;
                    List<long> needUpdateMaterialTypeMapIds = null;
                    if (raw.MATERIAL_TYPE_MAP_ID.HasValue)
                    {
                        currentMaterialTypeMapId = raw.MATERIAL_TYPE_MAP_ID.Value;
                        olds = new HisMaterialTypeGet().GetByMaterialTypeMapId(raw.MATERIAL_TYPE_MAP_ID.Value);
                        olds = olds != null ? olds.Where(o => o.ID != raw.ID).ToList() : null;
                    }

                    if (IsNotNullOrEmpty(news) && news.Any(a => a.MATERIAL_TYPE_MAP_ID.HasValue))
                    {
                        needUpdateMaterialTypeMapIds = news.Where(a => a.MATERIAL_TYPE_MAP_ID.HasValue).Select(s => s.MATERIAL_TYPE_MAP_ID.Value).Distinct().ToList();
                        if (!currentMaterialTypeMapId.HasValue)
                        {
                            currentMaterialTypeMapId = needUpdateMaterialTypeMapIds.FirstOrDefault();
                        }
                        if (currentMaterialTypeMapId.HasValue)
                        {
                            needUpdateMaterialTypeMapIds.Remove(currentMaterialTypeMapId.Value);
                        }
                    }

                    if (!currentMaterialTypeMapId.HasValue && IsNotNullOrEmpty(news))
                    {
                        this.ProcessorMaterialTypeMap(raw, ref currentMaterialTypeMapId);
                    }

                    List<HIS_MATERIAL_TYPE> lstSetNull = olds != null ? olds.Where(o => news == null || !news.Any(a => a.ID == o.ID)).ToList() : null;
                    List<HIS_MATERIAL_TYPE> listUpdate = new List<HIS_MATERIAL_TYPE>();
                    if (raw.MATERIAL_TYPE_MAP_ID != currentMaterialTypeMapId)
                    {
                        listUpdate.Add(raw);
                        raw.MATERIAL_TYPE_MAP_ID = currentMaterialTypeMapId;
                    }

                    if (IsNotNullOrEmpty(news))
                    {
                        listUpdate.AddRange(news);
                    }

                    if (IsNotNullOrEmpty(lstSetNull))
                    {
                        sqls.Add(DAOWorker.SqlDAO.AddInClause(lstSetNull.Select(s => s.ID).ToList(), "UPDATE HIS_MATERIAL_TYPE SET MATERIAL_TYPE_MAP_ID = NULL WHERE %IN_CLAUSE% ", "ID"));
                    }

                    if (IsNotNullOrEmpty(listUpdate))
                    {
                        string tempSql = DAOWorker.SqlDAO.AddInClause(listUpdate.Select(s => s.ID).ToList(), "UPDATE HIS_MATERIAL_TYPE SET MATERIAL_TYPE_MAP_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                        sqls.Add(String.Format(tempSql, currentMaterialTypeMapId.Value));
                    }

                    if (IsNotNullOrEmpty(needUpdateMaterialTypeMapIds))
                    {
                        string tempSql = DAOWorker.SqlDAO.AddInClause(needUpdateMaterialTypeMapIds, "UPDATE HIS_MATERIAL_TYPE SET MATERIAL_TYPE_MAP_ID = {0} WHERE %IN_CLAUSE% ", "MATERIAL_TYPE_MAP_ID");
                        sqls.Add(String.Format(tempSql, currentMaterialTypeMapId.Value));
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Sql: " + sqls.ToString());
                    }
                    result = true;
                    resultData = raw;
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.Rollback();
                result = false;
                param.HasException = true;
            }
            return result;
        }

        private void ProcessorMaterialTypeMap(HIS_MATERIAL_TYPE raw, ref long? currentMaterialTypeMapId)
        {
            HIS_MATERIAL_TYPE_MAP map = new HIS_MATERIAL_TYPE_MAP();
            map.MATERIAL_TYPE_MAP_CODE = raw.MATERIAL_TYPE_CODE;
            map.MATERIAL_TYPE_MAP_NAME = raw.MATERIAL_TYPE_NAME;

            if (!this.hisMaterialTypeMapCreate.Create(map))
            {
                throw new Exception("hisMaterialTypeMapCreate. Ket thuc nghiep vu");
            }
            currentMaterialTypeMapId = map.ID;
        }

        private void Rollback()
        {
            this.hisMaterialTypeMapCreate.RollbackData();
        }
    }
}
