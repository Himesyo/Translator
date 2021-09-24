using System;
using System.Threading;

namespace Himesyo.Translation
{
    /// <summary>
    /// 表示执行动作所需要的信息。
    /// </summary>
    public class ActionInfo
    {
        /// <summary>
        /// 取消动作的令牌。默认为 <see cref="CancellationToken.None"/>
        /// </summary>
        public CancellationToken Token { get; set; } = CancellationToken.None;

        /// <summary>
        /// 指示是否成功执行。
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 需要返回的消息。如果执行顺利，请为 <see langword="null"/> 。
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 执行中出现的异常。如果未出现异常，则为 <see langword="null"/> 。
        /// </summary>
        public Exception Exception { get; set; }
        /// <summary>
        /// 执行结果对象。如果无返回结果，则为 <see langword="null"/> 。
        /// </summary>
        public object Result { get; set; }

        /// <summary>
        /// 顺利完成，无任何意外状况。
        /// </summary>
        public void CompleteSuccess(object result = null)
        {
            Success = true;
            Message = null;
            Exception = null;
            Result = result;
        }
        /// <summary>
        /// 重置与结果有关的属性。
        /// </summary>
        /// <returns>返回此实例。</returns>
        public ActionInfo ResetResult()
        {
            Success = false;
            Message = null;
            Exception = null;
            Result = null;
            return this;
        }
    }
}
