using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    // Have Two of this. 
    /// <summary>
    /// A base provider for all commands in the InteractionSystem
    /// </summary>
    public abstract class InteractionCommandSystem : MonoBehaviour
    {
        public InteractionCommandSet Primary = new InteractionCommandSet();
        public InteractionCommandSet Secondary = new InteractionCommandSet();

        public InteractionCommandSet GetSet(EHandType handType)
        {
            return handType == EHandType.Primary ? Primary : Secondary;
        }

        private void Update()
        {
            Process();
        }

        public abstract void Process();
    }

    // Hand Type is added to abstract from Liminal SDK level.
    public enum EHandType
    {
        Primary,
        Secondary
    }

    public class InteractionCommandSet
    {
        public CommandBase Grab = new CommandBase();
        public CommandBase Drop = new CommandBase();
        public CommandBase Teleport = new CommandBase();
    }
}

