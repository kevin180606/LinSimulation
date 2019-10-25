using SimulationPTSystem.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using dr = System.Drawing;
using System.Configuration;
using System.Drawing;

namespace SimulationPTSystem.DataConverter.Screen
{
    public class DataConverter : IDataConverter
    {
        /// <summary>
        /// &&&sdlfaaaaaasdasdasd
        /// </summary>
        #region "events"
        public event EventHandler<DataReceivedEventArgs> DataReceived;
        #endregion
        #region "private members"
        private bool isRunning = false;
        private IntPtr hdc=IntPtr .Zero;
        private int nXPos = 0;
        private int nYPos = 0;
        private int _interval = 200;
        private dr.Color lastColor = dr.Color.FromArgb(0);
        #endregion
        #region "imports extern dlls"
        /// <summary>
        /// 获取指定窗口的设备场景
        /// </summary>
        /// <param name="hwnd">将获取其设备场景的窗口的句柄。若为0，则要获取整个屏幕的DC</param>
        /// <returns>指定窗口的设备场景句柄，出错则为0</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        /// <summary>
        /// 释放由调用GetDC函数获取的指定设备场景
        /// </summary>
        /// <param name="hwnd">要释放的设备场景相关的窗口句柄</param>
        /// <param name="hdc">要释放的设备场景句柄</param>
        /// <returns>执行成功为1，否则为0</returns>
        [DllImport("user32.dll")]
        public static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);
        /// <summary>
        /// 在指定的设备场景中取得一个像素的RGB值
        /// </summary>
        /// <param name="hdc">一个设备场景的句柄</param>
        /// <param name="nXPos">逻辑坐标中要检查的横坐标</param>
        /// <param name="nYPos">逻辑坐标中要检查的纵坐标</param>
        /// <returns>指定点的颜色</returns>
        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);
        #endregion
        #region "Constructor"
        /// <summary>
        /// 取控件
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="nXPos"></param>
        /// <param name="nYPos"></param>
        public DataConverter(IntPtr hdc, int nXPos = 0, int nYPos = 0)
        {
            this.hdc = hdc;
            this.nXPos = nXPos;
            this.nYPos = nYPos;

            Initialize();
        }
        /// <summary>
        /// 默认是取屏幕
        /// </summary>
        public DataConverter ():this(IntPtr.Zero,0,0)
        {
        }
        #endregion
        #region "public methods"
        public void Initialize()
        {
            string pointStr = ConfigurationManager.AppSettings["ScreenPoint"];
            if (!string.IsNullOrEmpty(pointStr))
            {
                string[] pointArray = pointStr.Split(',');
                if (pointArray.Length == 2)
                {
                    if(int.TryParse(pointArray[0],out int x)&&int.TryParse(pointArray[1],out int y))
                    { 
                        nXPos = x;
                        nYPos = y;
                    }
                }
            }
            if (int.TryParse(ConfigurationManager.AppSettings["Interval"], out int interval) && interval > 0)
            {
                _interval = interval;
            }
        }
        public Task<bool> StartAsync()
        {
            var task = Task.Run(() =>
            {
                this.isRunning = true;
                while (this.isRunning)
                {
                    Thread.Sleep(_interval);
                    dr.Color color = GetColor();
                    if (color.R != lastColor.R || color.G != lastColor.G || color.B != lastColor.B)
                    {
                        lastColor = color;
                        List<byte> lstBytes = new List<byte>();
                        lstBytes.Add(color.R);
                        lstBytes.Add(color.G);
                        lstBytes.Add(color.B);
                        this.DataReceived?.Invoke(this, new DataReceivedEventArgs(lstBytes.ToArray()));
                    }
                }
                return true;
            });
            return task;
        }
        public Task<bool> StopAsync()
        {
            var task = Task.Run(() =>
            {
                this.isRunning = false; return true;
            });
            return task;
        }
        #endregion
        #region "private methods"
        /// <summary>
        /// 获取指定句柄的控件的指定坐标处的像素的颜色
        /// </summary>
        /// <param name="hCtrlDC">控件句柄</param>
        /// <param name="x">像素x坐标</param>
        /// <param name="y">像素y坐标</param>
        /// <returns></returns>
        private  dr.Color GetColor()
        {
            IntPtr hdc = GetDC(this.hdc);
            uint pixel = GetPixel(hdc, this.nXPos, this.nYPos);
            ReleaseDC(this.hdc, hdc);
            dr.Color color = dr.Color.FromArgb((int)(pixel & 0x000000FF), (int)(pixel & 0x0000FF00) >> 8, (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }
        #endregion
    }
}
