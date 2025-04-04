using System.Collections;
using UnityEngine;
using Tarahiro;
using VContainer;

namespace Tarahiro.MasterData
{
    public abstract class MasterDataProvider<DataType, InterfaceType>
        : IMasterDataProvider<InterfaceType>
        where InterfaceType : IIndexable, IIdentifiable
        where DataType : InterfaceType
    {
        protected MasterDataOrderedDictionary<DataType, InterfaceType> m_Dictionary;



        [Inject]
        public MasterDataProvider()
        {
        }

        protected void Load(string filePath)
        {
            string path = MasterDataConst.DataPath + filePath;
            m_Dictionary = ResourceUtil.GetResource<MasterDataOrderedDictionary<DataType, InterfaceType>>(path);
        }

        public InterfaceType TryGetFromIndex(int index)
        {
            return m_Dictionary.TryGetFromIndex(index);
        }

        public InterfaceType TryGetFromId(string id)
        {
            return m_Dictionary.TryGetFromId(id);
        }

        public int Count { get { return m_Dictionary.Count; } }
    }
}