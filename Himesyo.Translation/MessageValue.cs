namespace Himesyo.Translation
{
    /// <summary>
    /// 表示一个消息对象。
    /// </summary>
    public class MessageValue
    {
        /// <summary>
        /// 用于同步的对象。
        /// </summary>
        public object SyncRoot { get; } = new object();
        /// <summary>
        /// 应该刷新值。
        /// </summary>
        public bool NeedRefresh { get; set; }
        /// <summary>
        /// 对象所携带的消息。
        /// </summary>
        public string Message { get; set; }
    }
}
