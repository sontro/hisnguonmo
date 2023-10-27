using ACS.EFMODEL.DataModels;
using ACS.LibraryEventLog;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsApplication;
using ACS.MANAGER.Core.AcsModuleGroup;
using ACS.MANAGER.Core.Check;
using ACS.MANAGER.EventLogUtil;
using ACS.UTILITY;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsModule.Update
{
    class AcsModuleUpdateBehaviorEv : BeanObjectBase, IAcsModuleUpdate
    {
        ACS_MODULE entity;
        private static string FORMAT_EDIT = "{0}: {1} => {2}";

        internal AcsModuleUpdateBehaviorEv(CommonParam param, ACS_MODULE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsModuleUpdate.Run()
        {
            bool result = false;
            try
            {
                ACS_MODULE oldData = null;
                result = Check(ref oldData) && DAOWorker.AcsModuleDAO.Update(entity);
                if (result)
                {
                    CreateEventLog(oldData);
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

        bool Check(ref  ACS_MODULE oldData)
        {
            bool result = true;
            try
            {
                result = result && AcsModuleCheckVerifyValidData.Verify(param, entity);
                result = result && AcsModuleCheckVerifyIsUnlock.Verify(param, entity.ID, ref oldData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void CreateEventLog(ACS_MODULE oldData)
        {
            try
            {
                List<string> editFields = new List<string>();

                editFields.Add(String.Format("MODULE_NAME: {0}", entity.MODULE_NAME));

                string co = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Co);
                string khong = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Khong);

                if (IsDiffLong(oldData.APPLICATION_ID, entity.APPLICATION_ID))
                {
                    ACS_APPLICATION old = new AcsApplicationBO().Get<ACS_APPLICATION>(oldData.APPLICATION_ID);
                    ACS_APPLICATION edit = new AcsApplicationBO().Get<ACS_APPLICATION>(entity.APPLICATION_ID);
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ApplicationName);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, old.APPLICATION_NAME, edit.APPLICATION_NAME));
                }

                if (IsDiffString(oldData.ICON_LINK, entity.ICON_LINK))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Icon);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ICON_LINK, entity.ICON_LINK));
                }

                if (IsDiffShortIsField(oldData.IS_ANONYMOUS, entity.IS_ANONYMOUS))
                {
                    string newValue = entity.IS_ANONYMOUS == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_ANONYMOUS == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.KhongPhanQuyen);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }

                //if (IsDiffShortIsField(oldData.IS_LEAF, entity.IS_LEAF))
                //{
                //    string newValue = entity.IS_LEAF == Constant.IS_TRUE ? co : khong;
                //    string oldValue = oldData.IS_LEAF == Constant.IS_TRUE ? co : khong;
                //    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Leaf);
                //    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                //}

                if (IsDiffShortIsField(oldData.IS_NOT_SHOW_DIALOG, entity.IS_NOT_SHOW_DIALOG))
                {
                    string newValue = entity.IS_NOT_SHOW_DIALOG == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_NOT_SHOW_DIALOG == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.KhongMoHopThoai);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }

                if (IsDiffShortIsField(oldData.IS_VISIBLE, entity.IS_VISIBLE))
                {
                    string newValue = entity.IS_VISIBLE == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_VISIBLE == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Menu);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }

                if (IsDiffLong(oldData.MODULE_GROUP_ID, entity.MODULE_GROUP_ID))
                {
                    ACS_MODULE_GROUP old = new AcsModuleGroupBO().Get<ACS_MODULE_GROUP>(oldData.MODULE_GROUP_ID ?? 0);
                    ACS_MODULE_GROUP edit = new AcsModuleGroupBO().Get<ACS_MODULE_GROUP>(entity.MODULE_GROUP_ID ?? 0);
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhomChucNang);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, IsNotNull(old) ? old.MODULE_GROUP_NAME : "", IsNotNull(edit) ? edit.MODULE_GROUP_NAME : ""));
                }

                if (IsDiffString(oldData.MODULE_LINK, entity.MODULE_LINK))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ModuleLink);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MODULE_LINK, entity.MODULE_LINK));
                }

                if (IsDiffString(oldData.MODULE_NAME, entity.MODULE_NAME))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ModuleName);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MODULE_NAME, entity.MODULE_NAME));
                }

                if (IsDiffString(oldData.MODULE_URL, entity.MODULE_URL))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Url);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MODULE_URL, entity.MODULE_URL));
                }

                if (IsDiffLong(oldData.NUM_ORDER, entity.NUM_ORDER))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoThuTu);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.NUM_ORDER, entity.NUM_ORDER));
                }

                if (IsDiffLong(oldData.PARENT_ID, entity.PARENT_ID))
                {
                    ACS_MODULE old = new AcsModuleBO().Get<ACS_MODULE>(oldData.PARENT_ID ?? 0);
                    ACS_MODULE edit = new AcsModuleBO().Get<ACS_MODULE>(entity.PARENT_ID ?? 0);
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Cha);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, IsNotNull(old) ? old.MODULE_NAME : "", IsNotNull(edit) ? edit.MODULE_NAME : ""));
                }

                if (IsDiffString(oldData.VIDEO_URLS, entity.VIDEO_URLS))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VideoUrls);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.VIDEO_URLS, entity.VIDEO_URLS));
                }

                new EventLogGenerator(EventLog.Enum.AcsModule_SuaDanhMucChucNang, string.Join(".", editFields))
                          .ModuleLink(entity.MODULE_LINK).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private static bool IsDiffString(string oldValue, string newValue)
        {
            return (oldValue ?? "") != (newValue ?? "");
        }
        private static bool IsDiffLong(long? oldValue, long? newValue)
        {
            return (oldValue ?? -1) != (newValue ?? -1);
        }
        private static bool IsDiffShortIsField(short? oldValue, short? newValue)
        {
            return (((oldValue == Constant.IS_TRUE) && (newValue != Constant.IS_TRUE)) || ((oldValue != Constant.IS_TRUE) && (newValue == Constant.IS_TRUE)));
        }
    }
}
