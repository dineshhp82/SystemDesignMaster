namespace TrafficSignalManagement.State
{
    public class EWGreenState : IState
    {
        public string Name => "EW_GREEN";

        public void Enter(IntersectionControl controller)
        {
            var ns = controller.GetSignal("NS");
            var ew = controller.GetSignal("EW");

            ns.SetLight(LightColor.Red, controller.Config.GreenDuration + controller.Config.YellowDuration);
            ew.SetLight(LightColor.Green, controller.Config.GreenDuration);

            controller.Notifier.NotifySignalChanged(ns);
            controller.Notifier.NotifySignalChanged(ew);

            Thread.Sleep(controller.Config.GreenDuration * 1000);
        }

        public void Exit(IntersectionControl controller)
        {
            
        }

        public IState Next(IntersectionControl controller) => new EWYellowState();
    }
}
