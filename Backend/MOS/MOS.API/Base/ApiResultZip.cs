﻿using Inventec.Common.Logging;
using Inventec.Core;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace MOS.API.Base
{
    public class ApiResultZip : IHttpActionResult
    {
        private object resultValue;
        private HttpActionContext actionContext;

        public ApiResultZip(object value, HttpActionContext context)
        {
            resultValue = value;
            actionContext = context;
        }

        /// <summary>
        /// Xu ly ket qua truoc khi tra ve client
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            string temp = "";
            try
            {
                HttpContent content = null;
                if (resultValue != null)
                {
                    if (resultValue.GetType() == typeof(bool) || resultValue.GetType() == typeof(Boolean))
                    {
                        temp = resultValue.ToString().ToLower();
                        content = new StringContent(temp);
                    }
                    else if (resultValue.GetType().IsPrimitive || resultValue.GetType() == typeof(string))
                    {
                        temp = resultValue + "";
                        content = new StringContent(temp);
                    }
                    else if (resultValue.GetType() == typeof(FileHolder))
                    {
                        FileHolder sr = (FileHolder)resultValue;
                        if (sr != null)
                        {
                            content = new StreamContent(sr.Content);
                            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                            content.Headers.ContentDisposition.FileName = sr.FileName;
                        }
                    }
                    else
                    {
                        var bin = MessagePack.MessagePackSerializer.Serialize(resultValue, MessagePack.Resolvers.ContractlessStandardResolver.Instance);
                        content = new ByteArrayContent(bin);
                    }
                }

                var response = new HttpResponseMessage()
                {
                    Content = content,
                    RequestMessage = actionContext.Request
                };
                //LogSystem.Debug(string.Format("Output: {0}", temp));
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return null;
        }
    }
}