using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keim.NetCore.DTO
{
    public abstract class MicroServiceManager
    {
        static MicroServiceManager()
        {
            ServiceItems = new MicroServiceCollection();
        }
        public static MicroServiceCollection ServiceItems
        { get; set; }

        public static bool IsCheckService(string ServiceTitle)
        {
            ServiceTitle.CheckNull("服务名称参数不可为空");
            return ServiceItems.ContainsService(ServiceTitle);
        }
    }


    public class MicroServiceCollection
    {
        private List<MicroServiceItem> Items = new List<MicroServiceItem>();

        public MicroServiceItem this[int index]
        {
            get
            {
                return Items[index];
            }
            set
            {
                Items[index] = value;
            }
        }

        public MicroServiceItem this[string ServiceTitle]
        {
            get
            {
                return Items.Where(v => v.ServiceTitle == ServiceTitle).FirstOrDefault();
            }
        }

        public int Count
        {
            get
            {
                return Items.Count;
            }
        }


        public void Add(MicroServiceItem item)
        {
            Items.Add(item);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(MicroServiceItem item)
        {
            return Items.Contains(item);
        }

        public bool ContainsService(string ServiceTitle)
        {
            return Items.Where(v => v.ServiceTitle == ServiceTitle.Trim()).Count() > 0;
        }

        public void CopyTo(MicroServiceItem[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<MicroServiceItem> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public int IndexOf(MicroServiceItem item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, MicroServiceItem item)
        {
            Items.Insert(index, item);
        }

        public bool Remove(MicroServiceItem item)
        {
            return Items.Remove(item);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }

    }

    public class MicroServiceItem
    {
        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServiceEndPoint
        { get; set; }

        /// <summary>
        /// 服务端口
        /// </summary>
        public int ServicePort
        { get; set; }

        /// <summary>
        /// 服务Uri
        /// </summary>
        public Uri ServiceUri
        {
            get
            {
                return new Uri($"http://{ServiceEndPoint}:{ServicePort}/");
            }
        }

        public override string ToString()
        {
            return $"http://{ServiceEndPoint}:{ServicePort}/";
        }

        /// <summary>
        /// 服务标题
        /// </summary>
        public string ServiceTitle
        { get; set; }

    }
}
