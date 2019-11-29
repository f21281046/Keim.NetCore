

namespace Keim.NetCore.MvvM
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    /// <summary>
    /// 
    /// 
    /// </summary>
    public abstract class AbstractNotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void RaisePropertyChanged(params string[] propertyNames)
        {
            if (propertyNames == null) throw new ArgumentNullException("propertyNames");

            foreach (var name in propertyNames)
            {
                this.RaisePropertyChanged(name);
            }
        }

        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = ExtractPropertyName(propertyExpression);
            this.RaisePropertyChanged(propertyName);
        }

        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("PropertySupport_NotMemberAccessExpression_Exception", "propertyExpression");
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("PropertySupport_ExpressionNotProperty_Exception", "propertyExpression");
            }

            var getMethod = property.GetMethod;
            if (getMethod.IsStatic)
            {
                throw new ArgumentException("PropertySupport_StaticExpression_Exception", "propertyExpression");
            }

            return memberExpression.Member.Name;
        }
    }
}
