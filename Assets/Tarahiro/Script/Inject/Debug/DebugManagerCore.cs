using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tarahiro
{
    #if ENABLE_DEBUG
    public class DebugManagerCore  : ITickable
    {
        public void Tick()
        {
            if (Input.GetKey(KeyCode.RightShift) && Input.GetKey(KeyCode.PageDown))
            {
                Time.timeScale = 5f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
    #endif
}