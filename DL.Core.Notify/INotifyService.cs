using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.Notify
{
    public interface INotifyService<TParmars> where TParmars: CommonParams
    {
        /// <summary>
        /// 通知类型
        /// </summary>
        NotifyType NotityType { get; }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <typeparam name="TParmars"></typeparam>
        /// <param name="parmars">公共参数</param>
        /// <returns>返回自定义发送结果</returns>
        object Send(TParmars parmars);


        //object Send<T>(TParmars parmars) where T: CommonParams;
        /// <summary>
        /// 发送信息(无任何返回值)
        /// </summary>
        /// <typeparam name="TParmars"></typeparam>
        /// <param name="parmars">公共参数</param>
        void SendVoid(TParmars parmars);

    }
}
