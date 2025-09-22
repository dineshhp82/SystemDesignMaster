namespace TrafficSignalManagement.State
{
    public interface IState
    {
        string Name { get; }

        void Enter(IntersectionControl controller);

        void Exit(IntersectionControl controller);

        IState Next(IntersectionControl controller);
    }
}
