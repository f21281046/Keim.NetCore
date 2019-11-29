using System;
using System.Windows.Input;

namespace Keim.NetCore.MvvM
{
    /// <summary>
    /// 事件处理机制
    /// </summary>
    public abstract class AbstractDelegateCommand : ICommand
    {
        //这个是要执行的事件
        public Action<object> ExecuteCommand = null;
        //这个是判断是否可执行
        public Func<object, bool> CanExecuteCommand = null;
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            if (CanExecuteCommand != null)
            {
                return this.CanExecuteCommand(parameter);
            }
            else
            {
                return true;
            }
        }

        public virtual void Execute(object parameter)
        {
            if (this.ExecuteCommand != null) this.ExecuteCommand(parameter);
        }

        public virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
