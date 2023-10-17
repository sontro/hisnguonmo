using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.MANAGER.Core.SdaConfigAppUser.Copy
{
    class SdaConfigAppUserCopyByConfigBehavior : BeanObjectBase, ISdaConfigAppUserCopy
    {
        private SDO.SdaConfigAppUserCopyByConfigSDO entity;

        public SdaConfigAppUserCopyByConfigBehavior(Inventec.Core.CommonParam param, SDO.SdaConfigAppUserCopyByConfigSDO sdaConfigAppUserCopyByConfigSDO)
            : base(param)
        {
            this.entity = sdaConfigAppUserCopyByConfigSDO;
        }

        object ISdaConfigAppUserCopy.Run()
        {
            List<SDA_CONFIG_APP_USER> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(entity);
                valid = valid && IsNotNull(entity.CopyConfigId);
                valid = valid && IsNotNull(entity.PasteConfigId);
                if (valid)
                {
                    List<SDA_CONFIG_APP_USER> newDatas = new List<SDA_CONFIG_APP_USER>();

                    SdaConfigAppUserBO bo = new SdaConfigAppUserBO();
                    bo.CopyCommonParamInfoGet(param);

                    Get.SdaConfigAppUserFilterQuery copyFilter = new Get.SdaConfigAppUserFilterQuery();
                    copyFilter.CONFIG_APP_ID = entity.CopyConfigId;
                    List<SDA_CONFIG_APP_USER> copyDatas = bo.Get<List<SDA_CONFIG_APP_USER>>(copyFilter);

                    Get.SdaConfigAppUserFilterQuery pasteFilter = new Get.SdaConfigAppUserFilterQuery();
                    pasteFilter.CONFIG_APP_ID = entity.PasteConfigId;
                    List<SDA_CONFIG_APP_USER> pasteDatas = bo.Get<List<SDA_CONFIG_APP_USER>>(pasteFilter);

                    if (!IsNotNullOrEmpty(copyDatas))
                    {
                        SDA_CONFIG_APP report = new Manager.SdaConfigAppManager(param).Get<SDA_CONFIG_APP>(entity.CopyConfigId);
                        string name = report != null ? report.KEY : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.SdaConfigAppUser_CauHinhChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu CopyConfig");
                    }

                    foreach (SDA_CONFIG_APP_USER copyData in copyDatas)
                    {
                        SDA_CONFIG_APP_USER data = pasteDatas != null ? pasteDatas.FirstOrDefault(o => o.CONFIG_APP_ID == copyData.CONFIG_APP_ID) : null;
                        if (data != null)
                        {
                            continue;
                        }
                        else
                        {
                            data = new SDA_CONFIG_APP_USER();
                            data.CONFIG_APP_ID = entity.CopyConfigId;
                            data.LOGINNAME = copyData.LOGINNAME;
                            newDatas.Add(data);
                        }
                    }
                    if (IsNotNullOrEmpty(newDatas))
                    {
                        if (!Base.DAOWorker.SdaConfigAppUserDAO.CreateList(newDatas))
                        {
                            throw new Exception("Khong tao duoc SDA_CONFIG_APP_USER");
                        }
                    }

                    result = new List<SDA_CONFIG_APP_USER>();
                    if (IsNotNullOrEmpty(newDatas))
                    {
                        result.AddRange(newDatas);
                    }
                    if (IsNotNullOrEmpty(pasteDatas))
                    {
                        result.AddRange(pasteDatas);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }
    }
}
