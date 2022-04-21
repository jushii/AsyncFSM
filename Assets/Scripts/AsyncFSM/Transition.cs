using System;

namespace AsyncFSM
{
    public class Transition : Transition<Options>
    {
        public Transition(Type type, Options options) : base(type, options)
        {
        }
    }

    public abstract class Transition<T> where T : Options
    {
        public Type Type { get; }
        public T Options { get; }

        protected Transition(Type type, T options)
        {
            Type = type;
            Options = options;
        }
    }
}