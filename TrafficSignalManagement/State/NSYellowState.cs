namespace TrafficSignalManagement.State
{
    internal class NSYellowState : IState
    {
        public string Name => "NS_YELLOW";

        public void Enter(IntersectionControl controller)
        {
            var ns = controller.GetSignal("NS");
            var ew = controller.GetSignal("EW");

            ns.SetLight(LightColor.Yellow, controller.Config.YellowDuration);
            ew.SetLight(LightColor.Red, controller.Config.YellowDuration);

            controller.Notifier.NotifySignalChanged(ns);
            controller.Notifier.NotifySignalChanged(ew);

            Thread.Sleep(controller.Config.YellowDuration * 1000);
        }

        public void Exit(IntersectionControl controller)
        {
            throw new NotImplementedException();
        }

        public IState Next(IntersectionControl controller)
        {
            throw new NotImplementedException();
        }
    }
}
