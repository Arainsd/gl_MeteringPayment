using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Module.BootLoader
{
    /// <summary>
    /// 编辑表单内容暴露一个事件刷新列表内容
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EditFormEventArgs<T> : EventArgs
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public OperationType Operation { get; set; }
        /// <summary>
        /// 操作成功后的数据对象
        /// </summary>
        public T Data { get; set; }
        public EditFormEventArgs(OperationType operation, T data)
        {
            this.Operation = operation;
            this.Data = data;
        }
    }
    public enum OperationType
    {
        /// <summary>
        /// 添加
        /// </summary>
        Add,
        /// <summary>
        /// 修改
        /// </summary>
        Update,
        /// <summary>
        /// 删除
        /// </summary>
        Delete,
        /// <summary>
        /// 禁用
        /// </summary>
        Disable,
        /// <summary>
        /// 启用
        /// </summary>
        Enable,
        /// <summary>
        /// 发布
        /// </summary>
        Fix,
        /// <summary>
        /// 撤销发布
        /// </summary>
        UnFix
    }
}
