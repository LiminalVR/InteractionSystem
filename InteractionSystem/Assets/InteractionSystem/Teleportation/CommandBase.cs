namespace Liminal.SDK.InteractableSystem
{
    /// <summary>
    /// A slight modification to the common command pattern where there is an Action to add actions to.
    /// </summary>
    public class CommandBase
    {
        public bool Down { get; set; }
        public bool Pressed { get; set; }
        public bool Up { get; set; }
    }
}