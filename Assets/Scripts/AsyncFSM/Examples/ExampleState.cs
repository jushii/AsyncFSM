using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AsyncFSM.Examples
{
    public class ExampleState : State
    {
        public override async UniTask OnEnter()
        {
            Debug.Log("Entering ExampleState! Waiting 2 seconds before changing state.");

            await UniTask.Delay(2000);

            var options = new ExampleStateOptions
            {
                text = "Hello world!"
            };

            StateMachine.RequestTransition(typeof(ExampleStateWithOptions), options);
        }

        public override async UniTask OnExit()
        {
            Debug.Log("Exiting ExampleState!");

            await UniTask.Yield();
        }

        public override void OnUpdate()
        {
            // This is never called because we request a transition in OnEnter.       
            Debug.Log("Calling OnUpdate in ExampleState!");
        }
    }
}