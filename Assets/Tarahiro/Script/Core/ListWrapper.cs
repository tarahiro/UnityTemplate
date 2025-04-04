using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;

namespace Tarahiro
{

    [System.Serializable]
    public class ListWrapper<T>
    {
        [SerializeField] List<T> _wrappedList;

        public List<T> GetList()
        {
            return _wrappedList;
        }

        public ListWrapper(List<T> list)
        {
           _wrappedList = list;
        }
    }
}
