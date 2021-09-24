namespace Himesyo.Translation
{
    /// <summary>
    /// 表示当前文件所在的状态。实现时无需使用所有预设的状态，也可以添加自定义状态。
    /// </summary>
    public class FileState
    {
        /// <summary>
        /// 未设置状态。未知状态。
        /// <para>Value = "", Name = "未知"</para>
        /// </summary>
        public static FileState None { get; } = new FileState("", "未知");
        /// <summary>
        /// 新建。
        /// <para>Value = "Create", Name = "创建"</para>
        /// </summary>
        public static FileState Create { get; } = new FileState("Create", "创建");
        /// <summary>
        /// 正在初始化。
        /// <para>Value = "Init", Name = "初始化"</para>
        /// </summary>
        public static FileState Init { get; } = new FileState("Init", "初始化");
        /// <summary>
        /// 就绪。
        /// <para>Value = "Ready", Name = "就绪"</para>
        /// </summary>
        public static FileState Ready { get; } = new FileState("Ready", "就绪");


        /// <summary>
        /// 状态值。
        /// </summary>
        public string Value { get; }
        /// <summary>
        /// 状态显示名称。
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// 使用指定值初始化新实例。
        /// </summary>
        /// <param name="value"></param>
        protected FileState(string value) : this(value, value)
        {

        }
        /// <summary>
        /// 使用指定值初始化新实例。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        public FileState(string value, string name)
        {
            ExceptionHelper.ThrowNull(value, nameof(value));
            Value = value;
            Name = name;
        }

        /// <summary>
        /// 比较状态值是否一致。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            FileState state = obj as FileState;
            return Value == state?.Value;
        }
        /// <summary>
        /// 获取状态值的哈希代码。
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        /// <summary>
        /// 可读的字符串形式。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return Value;
            }
            else
            {
                return Name;
            }
        }

        /// <inheritdoc/>
        public static bool operator ==(FileState left, FileState right)
        {
            if (left is null && right is null)
                return true;
            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }
        /// <inheritdoc/>
        public static bool operator !=(FileState left, FileState right)
        {
            if (left is null && right is null)
                return false;
            if (left is null || right is null)
                return true;

            return !left.Equals(right);
        }
    }
}
