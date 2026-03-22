
using gaw241201.View;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static Tarahiro.TInput.TouchConst;

namespace Tarahiro.TInput
{
#if ENABLE_VIRTUAL_CURSOR

    //とりあえずテストとしてfixを実装

    public class MonoVirtualCursor : MonoBehaviour
    {

        //FakeCanvas
        //const string c_prefabName = "Prefab/Cursor";
        const string c_objectName = "VirtualCursor";
        Transform _cursorTransform;

        private void Awake()
        {
            //_cursorTransform = _resolver.Instantiate(UtilResource.GetResource<Transform>(c_prefabName));
            _cursorTransform = GameObject.Find(c_objectName)?.transform;
            TCanvas.GetInstance().RegisterInstance(_cursorTransform, Const.OrderOnMonoCanvas.Cursor);
        }


    //エディタ上では表示がずれるので、カーソルfixは一旦しない
        private void Update()
        {
#if UNITY_EDITOR
            _cursorTransform.localPosition = TTouch.GetInstance().ScreenPointOnThisFrame - WindowSizeGetterStatic.Get() / 2;
            /*
            if (_isCursorFix)
            {
                _cursorTransform.localPosition = _fixedPosition - WindowSizeGetterStatic.Get() / 2;
            }
            else
            {
                _cursorTransform.localPosition = TTouch.GetInstance().ScreenPointOnThisFrame - WindowSizeGetterStatic.Get() / 2;
            }
            */

#else
            _cursorTransform.localPosition = TTouch.GetInstance().ScreenPointOnThisFrame - WindowSizeGetterStatic.Get() / 2;
#endif
        }

        bool _isCursorFix = false;
        Vector2 _fixedPosition;

        public void CursorFix(Vector2 unityWindowPosition)
        {
            _isCursorFix = true;

            //外部から渡さないと、位置ずれるかも
            _fixedPosition = unityWindowPosition;
        }

        public void CursorUnfix()
        {
            _isCursorFix = false;
        }
    }
#endif

}