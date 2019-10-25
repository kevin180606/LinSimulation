using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimulationPTSystem.Common.Interface
{
    /// <summary>
    /// 数据转换器接口
    /// </summary>
    public interface IDataConverter
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize();
        /// <summary>
        /// 异步开始接收数据
        /// </summary>
        /// <returns></returns>
        Task<bool> StartAsync();
        /// <summary>
        /// 异步停止接收数据
        /// </summary>
        /// <returns></returns>
        Task<bool> StopAsync();
        /// <summary>
        /// 数据接收事件
        /// </summary>
        event EventHandler<DataReceivedEventArgs> DataReceived;
    }

    /// <summary>
    /// 数据接收事件的事件参数
    /// </summary>
    public class DataReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// 接收到的数据
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 数据接收事件参数构造函数
        /// </summary>
        /// <param name="data">接收到的数据</param>
        public DataReceivedEventArgs(byte[] data)
        {
            Data = data;
        }
    }
}
