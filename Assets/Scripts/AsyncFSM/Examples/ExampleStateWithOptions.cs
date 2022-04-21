using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AsyncFSM.Examples
{
    public class ExampleStateOptions : Options
    {
        public string text;
    }

    public class ExampleStateWithOptions : State<ExampleStateOptions>
    {
        public override async UniTask OnEnter()
        {
            Debug.Log($"Entering ExampleStateWithOptions. Here's our options text: {Options.text}");

            await UniTask.Yield();
        }

        public override void OnUpdate()
        {
            Debug.Log($"realTimeSinceStartup: {Time.realtimeSinceStartup}, frameCount:{Time.frameCount}");
        }
    }
}