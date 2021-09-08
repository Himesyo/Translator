namespace Himesyo.Translation
{
    /// <summary>
    /// 为动作执行期间发送实时消息的功能提供基类。
    /// </summary>
    public class ActionRunMessage : ActionRun
    {
        /// <summary>
        /// 添加消息接收器。
        /// </summary>
        /// <param name="messageValue"></param>
        public void AddMessageValue(MessageValue messageValue)
        {
            MessageChanged += (_, msg) =>
            {
                messageValue.Message = msg;
                messageValue.NeedRefresh = true;
            };
        }

        /// <summary>
        /// 实时消息改变时触发。
        /// </summary>
        public event MessageChangedEventHandler MessageChanged;

        /// <summary>
        /// 显示给用户的实时信息。
        /// </summary>
        public string Message
        {
            get => StagingMessage;
            set
            {
                StagingMessage = value;
                RefreshMessage();
            }
        }
        /// <summary>
        /// 暂存消息但不主动通知。
        /// </summary>
        public string StagingMessage { get; set; }

        /// <summary>
        /// 提示界面应刷新消息。
        /// </summary>
        public virtual void RefreshMessage()
        {
            MessageChanged?.Invoke(this, StagingMessage);
        }
    }

    /// <summary>
    /// 实时消息改变时触发的事件所使用的委托。
    /// </summary>
    /// <typeparam name="TRun"></typeparam>
    /// <param name="run"></param>
    /// <param name="newMessage"></param>
    public delegate void MessageChangedEventHandler(ActionRunMessage run, string newMessage);
}
