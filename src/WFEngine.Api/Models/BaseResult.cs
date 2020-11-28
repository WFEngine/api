using System;
using System.Net;

namespace WFEngine.Api.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResult<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseResult()
        {
            Data = (T)Activator.CreateInstance(typeof(T));
            Message = default;
            StatusCode = HttpStatusCode.OK;
        }
    }
}
