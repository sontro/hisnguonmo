using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaGroup.CreateWithUpdatePath
{
    class SdaGroupCreateUpdatePathBehavior : BeanObjectBase, ISdaGroupCreateWithUpdatePath
    {
        SDA_GROUP entity;
        private string separatePath = "|";

        internal SdaGroupCreateUpdatePathBehavior(CommonParam param, SDA_GROUP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaGroupCreateWithUpdatePath.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaGroupDAO.Create(entity);
                if (result)
                {
                    if (!UpdatePath(entity))
                    {
                        Inventec.Common.Logging.LogSystem.Error("Update path cho group thanh cong. De nghi van hanh khai thac thong bao & xu ly ngay lap tuc.");
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && SdaGroupCheckVerifyValidData.Verify(param, entity);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private bool UpdatePath(SDA_GROUP data)
        {
            bool result = false;
            try
            {
                if (data.PARENT_ID.HasValue)
                {
                    SDA_GROUP parent = DAOWorker.SdaGroupDAO.GetById(data.PARENT_ID.Value, new SDA.DAO.StagingObject.SdaGroupSO());
                    if (parent != null)
                    {
                        data.ID_PATH = parent.ID_PATH + data.ID + separatePath;
                        data.CODE_PATH = parent.CODE_PATH + data.G_CODE + separatePath;
                        result = DAOWorker.SdaGroupDAO.Update(data);
                    }
                    else
                    {
                        Logging("Khong tim duoc parent de cap nhat path." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), LogType.Error);
                    }
                }
                else
                {
                    data.ID_PATH = separatePath + data.ID + separatePath;
                    data.CODE_PATH = separatePath + data.G_CODE + separatePath;
                }

                if (DAOWorker.SdaGroupDAO.Update(data))
                {
                    result = true;
                }
                else
                {
                    BugUtil.SetBugCode(param, SDA.LibraryBug.Bug.Enum.SdaGroup_KhongUpdateDuocGroupPath);
                    if (!DAOWorker.SdaGroupDAO.Truncate(data))
                    {
                        BugUtil.SetBugCode(param, SDA.LibraryBug.Bug.Enum.SdaGroup_DAOTruncateGroupThatBai);
                        Logging("Khong truncate duoc group, sau khi thuc hien update path that bai." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), LogType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
