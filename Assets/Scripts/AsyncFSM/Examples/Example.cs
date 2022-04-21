using UnityEngine;

namespace AsyncFSM.Examples
{
    public static class Example
    {
        private static StateMachine _stateMachine;

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            _stateMachine = new StateMachine();
            _stateMachine.RegisterState(new ExampleState());
            _stateMachine.RegisterState(new ExampleStateWithOptions());
            _stateMachine.RequestTransition(typeof(ExampleState));
            _stateMachine.Run();
        }
    }
}