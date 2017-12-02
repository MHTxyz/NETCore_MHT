using System;
using System.Collections.Generic;
using System.Text;

namespace NETCoreExp
{
    /// <summary>
    /// 子类
    /// </summary>
    sealed class FinalizeDisposeSon : FinalizeDisposeBase
    {
        private bool disposed = false;

        public FinalizeDisposeSon()
        {

        }

        ~FinalizeDisposeSon()
        {
            Dispose(false);
        }

        protected override void Dispose(bool isDisposing)
        {
            if (disposed)
            {
                return;
            }

            if (isDisposing)
            {
                //在此释放在这个类中声明的托管资源
            }

            //在这里释放在此类中声明的非托管资源...

            //调用父类的Dispose方法来释放父类中的资源
            base.Dispose(isDisposing);

            //标记已释放
            disposed = true;
        }
    }
}
