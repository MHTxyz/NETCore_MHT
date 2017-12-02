using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace NETCoreExp
{
    /// <summary>
    /// 父类
    /// </summary>
    class FinalizeDisposeBase : IDisposable
    {
        //演示创建一个非托管资源
        private IntPtr nativeResource = Marshal.AllocHGlobal(100);
        //演示创建一个托管资源
        private AnotherResource managedResource = new AnotherResource();
        //标记对象是否已被释放
        private bool disposed = false;

        //private string filePath = "C:/test.txt";
        //private FileStream fs;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FinalizeDisposeBase()
        {
            //try
            //{
            //    fs = File.Create(filePath);
            //    //using (fs = File.Create(filePath))
            //    //{
            //    //}
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        /// <summary>
        /// 析构函数，(终结器)以备忘记了显式调用Dispose方法
        /// </summary>
        ~FinalizeDisposeBase()
        {
            Dispose(false);
        }

        /// <summary>
        /// 实现IDisposable中的Dispose方法
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            //告诉GC此对象的Finalize方法不再需要调用
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 在这里做实际的析构工作
        /// 申明为虚方法以供子类在必要时重写
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (disposed)
            {
                return;
            }

            if (isDisposing)
            {
                //释放托管资源
                if (managedResource != null)
                {
                    managedResource.Dispose();
                    managedResource = null;
                }
                //if (fs != null)
                //{
                //    fs.Dispose();
                //    fs = null;

                //    if (File.Exists(filePath))
                //    {
                //        File.Delete(filePath);
                //    }
                //}
            }

            //清理非托管资源...
            if (nativeResource != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(nativeResource);
                nativeResource = IntPtr.Zero;
            }

            //标记已释放
            disposed = true;
        }
    }
}
