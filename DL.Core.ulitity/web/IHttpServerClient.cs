﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DL.Core.ulitity.web
{
   public interface IHttpServerClient:IDisposable
    {
        HttpClient HttpClient { get; set; }

        /// <summary>
        /// 设置token
        /// </summary>
        /// <param name="token"></param>
        void SetBearerToken(string token);
        /// <summary>
        /// 设置请求host
        /// </summary>
        /// <param name="address"></param>
        void SetAddressUrl(string address = null);
        /// <summary>
        /// Get请求网络Api接口(Query)
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="pairs">请求参数</param>
        /// <returns></returns>
        Task<string> GetApiAsync(string url, Dictionary<string, string> pairs = null);

        /// <summary>
        /// Get请求网络Api接口(Query)
        /// </summary>
        /// <typeparam name="T">返回类型,通常是class</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="pairs">请求参数</param>
        /// <returns></returns>
        Task<T> GetApiAsync<T>(string url, Dictionary<string, string> pairs = null) where T : class;

        /// <summary>
        /// Get请求网络Api接口(Query)
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="pairs">请求参数</param>
        /// <returns></returns>
        string GetApi(string url, Dictionary<string, string> pairs = null);

        /// <summary>
        /// Get请求网络Api接口(Query)
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="pairs">请求参数</param>
        /// <returns></returns>
        T GetApi<T>(string url, Dictionary<string, string> pairs = null) where T : class;

        /// <summary>
        /// Post请求网络Api接口
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="pairs">请求参数</param>
        /// <returns></returns>
        Task<string> PostApiAsync(string url, Dictionary<string, object> pairs = null);

        /// <summary>
        /// Post请求网络Api接口
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        Task<string> PostApiAsync(string url, string json = null);

        /// <summary>
        /// Post请求网络Api接口
        /// </summary>
        /// <typeparam name="T">返回参数</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="pairs">请求参数</param>
        /// <returns></returns>
        Task<T> PostApiAsync<T>(string url, Dictionary<string, object> pairs = null) where T : class;

        /// <summary>
        /// Post请求网络Api接口
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        Task<T> PostApiAsync<T>(string url, string json = null) where T : class;

        /// <summary>
        /// Post请求网络Api接口
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="pairs">请求参数</param>
        /// <returns></returns>
        string PostApi(string url, Dictionary<string, object> pairs = null);

        /// <summary>
        /// Post请求网络Api接口
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="json">json字符串</param>
        string PostApi(string url, string json = null);

        /// <summary>
        /// Post请求网络Api接口
        /// </summary>
        /// <typeparam name="T">返回参数</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="pairs">请求参数</param>
        /// <returns></returns>
        T PostApi<T>(string url, Dictionary<string, object> pairs = null) where T : class;

        /// <summary>
        /// Post请求网络Api接口
        /// </summary>
        /// <typeparam name="T">返回参数</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        T PostApi<T>(string url, string json = null) where T : class;
    }
}
