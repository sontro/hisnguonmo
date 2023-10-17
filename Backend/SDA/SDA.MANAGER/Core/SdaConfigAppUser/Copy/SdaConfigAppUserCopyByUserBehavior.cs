using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.MANAGER.Core.SdaConfigAppUser.Copy
{
    class SdaConfigAppUserCopyByUserBehavior : BeanObjectBase, ISdaConfigAppUserCopy
    {
        private SDO.SdaConfigAppUserCopyByUserSDO entity;

        public SdaConfigAppUserCopyByUserBehavior(Inventec.Core.CommonParam param, SDO.SdaConfigAppUserCopyByUserSDO sdaConfigAppUserCopyByUserSDO)
            : base(param)
        {
            this.entity = sdaConfigAppUserCopyByUserSDO;
        }

        object ISdaConfigAppUserCopy.Run()
        {
            List<SDA_CONFIG_APP_USER> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(entity);
                valid = valid && IsNotNull(entity.CopyUserLoginname);
                valid = valid && IsNotNull(entity.PasteUserLoginname);
                if (valid)
                {
                    List<SDA_CONFIG_APP_USER> newDatas = new List<SDA_CONFIG_APP_USER>();

                    SdaConfigAppUserBO bo = new SdaConfigAppUserBO();
                    bo.CopyCommonParamInfoGet(param);

                    Get.SdaConfigAppUserFilterQuery copyFilter = new Get.SdaConfigAppUserFilterQuery();
                    copyFilter.LOGINNAME = entity.CopyUserLoginname;
                    List<SDA_CONFIG_APP_USER> copyDatas = bo.Get<List<SDA_CONFIG_APP_USER>>(copyFilter);

                    Get.SdaConfigAppUserFilterQuery pasteFilter = new Get.SdaConfigAppUserFilterQuery();
                    pasteFilter.LOGINNAME = entity.PasteUserLoginname;
                    List<SDA_CONFIG_APP_USER> pasteDatas = bo.Get<List<SDA_CONFIG_APP_USER>>(pasteFilter);

                    if (!IsNotNullOrEmpty(copyDatas))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.SdaConfigAppUser_NguoiDungChuaCoDuLieuThietLap, entity.CopyUserLoginname);
                        throw new Exception("Khong co du lieu CopyUserLoginname");
                    }

                    foreach (SDA_CONFIG_APP_USER copyData in copyDatas)
                    {
                        SDA_CONFIG_APP_USER data = pasteDatas != null ? pasteDatas.FirstOrDefault(o => o.LOGINNAME == copyData.LOGINNAME) : null;
                        if (data != null)
                        {
                            continue;
                        }
                        else
                        {
                            data = new SDA_CONFIG_APP_USER();
                            data.LOGINNAME = entity.PasteUserLoginname;
                            data.CONFIG_APP_ID = copyData.CONFIG_APP_ID;
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
