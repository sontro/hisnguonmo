using Inventec.Common.Logging;
using ACS.EFMODEL.DataModels;
using ACS.LibraryEventLog;
using ACS.LogManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ACS.MANAGER.EventLogUtil
{
    class EventLogGenerator
    {
        private EventLog.Enum logEnum;
        private object[] extraParams;

        public EventLogGenerator(EventLog.Enum en, params object[] extras)
        {
            this.logEnum = en;
            this.extraParams = extras;
        }

        [SimpleEventKey(SimpleEventKey.APPLICATION_CODE)]
        private string applicationCode;

        public EventLogGenerator ApplicationCode(string p)
        {
            this.applicationCode = p;
            return this;
        }


        [SimpleEventKey(SimpleEventKey.ROLE_CODE)]
        private string roleCode;

        public EventLogGenerator RoleCode(string p)
        {
            this.roleCode = p;
            return this;
        }

        [SimpleEventKey(SimpleEventKey.MODULE_LINK)]
        private string moduleLink;

        public EventLogGenerator ModuleLink(string p)
        {
            this.moduleLink = p;
            return this;
        }              

        [ComplexEventKey(ComplexEventKey.APPLICATION_ROLE_DATA)]
        private ApplicationRoleData applicationRoleData;

        public EventLogGenerator ApplicationRoleData(ApplicationRoleData p)
        {
            this.applicationRoleData = p;
            return this;
        }


        [ComplexEventKey(ComplexEventKey.ROLE_DATA)]
        private RoleData roleData;

        public EventLogGenerator RoleData(RoleData p)
        {
            this.roleData = p;
            return this;
        }

        [ComplexEventKey(ComplexEventKey.ROLE_BASE_DATA)]
        private RoleBaseData roleBaseData;

        public EventLogGenerator RoleBaseData(RoleBaseData p)
        {
            this.roleBaseData = p;
            return this;
        }

        [ComplexEventKey(ComplexEventKey.ROLE_LIST_DATA)]
        private RoleListData roleListData;

        public EventLogGenerator RoleListData(RoleListData p)
        {
            this.roleListData = p;
            return this;
        }


        [ComplexEventKey(ComplexEventKey.MODULE_ROLE_DATA)]
        private ModuleRoleData moduleRoleData;

        public EventLogGenerator ModuleRoleData(ModuleRoleData p)
        {
            this.moduleRoleData = p;
            return this;
        }

        public void Run()
        {
            try
            {
                string logContent = LogCommonUtil.GetEventLogContent(this.logEnum);

                if (!string.IsNullOrWhiteSpace(logContent))
                {
                    if (this.extraParams != null && this.extraParams.Length > 0)
                    {
                        logContent = String.Format(logContent, extraParams);
                    }

                    FieldInfo[] fields = typeof(EventLogGenerator).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                    foreach (FieldInfo field in fields)
                    {
                        if (field.CustomAttributes != null && field.CustomAttributes.Count() > 0)
                        {
                            SimpleEventKey simpleEventKey = (SimpleEventKey)field.GetCustomAttribute(typeof(SimpleEventKey), false);
                            ComplexEventKey complexEventKey = (ComplexEventKey)field.GetCustomAttribute(typeof(ComplexEventKey), false);

                            string val = "";

                            if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
                            {
                                IEnumerable list = (IEnumerable)field.GetValue(this);
                                if (list != null)
                                {
                                    foreach (var t in list)
                                    {
                                        if (t != null)
                                        {
                                            val += string.IsNullOrWhiteSpace(val) ? t.ToString() : "; " + t.ToString();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                val = field.GetValue(this) != null ? field.GetValue(this).ToString() : "";
                            }

                            if (simpleEventKey != null)
                            {
                                val = !string.IsNullOrWhiteSpace(val) ? string.Format("{0}: {1}", simpleEventKey.Value, val) : val;
                                logContent = logContent.Replace("%" + simpleEventKey.Value + "%", val);
                            }
                            if (complexEventKey != null)
                            {
                                logContent = logContent.Replace("%" + complexEventKey.Value + "%", val);
                            }
                        }
                    }

                    EventLogCache.Push(logContent);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
