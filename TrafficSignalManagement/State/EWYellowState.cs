namespace TrafficSignalManagement.State
{
    public class EWYellowState : IState
    {
        public string Name => "EW_YELLOW";

        public void Enter(IntersectionControl controller)
        {
            var ns = controller.GetSignal("NS");
            var ew = controller.GetSignal("EW");

            ns.SetLight(LightColor.Red, controller.Config.YellowDuration);
            ew.SetLight(LightColor.Yellow, controller.Config.YellowDuration);

            controller.Notifier.NotifySignalChanged(ns);
            controller.Notifier.NotifySignalChanged(ew);

            Thread.Sleep(controller.Config.YellowDuration * 1000);
        }

        public void Exit(IntersectionControl controller)
        {
           
        }

        public IState Next(IntersectionControl controller)
        {
            return new NSGreenState();
        }
    }
}