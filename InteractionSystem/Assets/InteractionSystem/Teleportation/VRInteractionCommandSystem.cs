using System;
using System.Collections.Generic;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;

namespace Liminal.SDK.InteractableSystem
{
    /// <summary>
    /// A concrete implementation for Liminal SDK interaction System.
    /// </summary>
    public class VRInteractionCommandSystem : InteractionCommandSystem
    {
        public InteractionMap PrimaryMap;
        public InteractionMap SecondaryMap;

        public override void Process()
        {
            var device = VRDevice.Device;
            if (device == null)
                return;

            ProcessInput(device.PrimaryInputDevice, PrimaryMap.TeleportButtons, Primary.Teleport);
            ProcessInput(device.SecondaryInputDevice, SecondaryMap.TeleportButtons, Secondary.Teleport);

            ProcessInput(device.PrimaryInputDevice, PrimaryMap.GrabButtons, Primary.Grab);
            ProcessInput(device.SecondaryInputDevice, SecondaryMap.GrabButtons, Secondary.Grab);

            ProcessInput(device.PrimaryInputDevice, PrimaryMap.UseButtons, Primary.Use);
            ProcessInput(device.SecondaryInputDevice, SecondaryMap.UseButtons, Secondary.Use);

            ProcessInput(device.PrimaryInputDevice, PrimaryMap.DropButtons, Primary.Drop);
            ProcessInput(device.SecondaryInputDevice, SecondaryMap.DropButtons, Secondary.Drop);
        }

        public void ProcessInput(IVRInputDevice device, List<EButtonType> buttonTypes, CommandBase command)
        {
            if (device == null) 
                return;

            command.Down = GetButton(device, buttonTypes, EButtonState.Down);
            command.Pressed = GetButton(device, buttonTypes, EButtonState.Pressed);
            command.Up = GetButton(device, buttonTypes, EButtonState.Up);
        }

        public bool GetButton(IVRInputDevice device, List<EButtonType> buttonTypes, EButtonState state)
        {
            foreach (var eButtonType in buttonTypes)
            {
                switch (eButtonType)
                {
                    case EButtonType.MiddleMouse:
                    case EButtonType.LeftMouse:
                    case EButtonType.RightMouse:
                        var mouseNumber = GetMouseNumber(eButtonType);
                        if (UnityEngine.Input.GetMouseButtonDown(mouseNumber) && state == EButtonState.Down)
                            return true;
                        if (UnityEngine.Input.GetMouseButton(mouseNumber) && state == EButtonState.Pressed)
                            return true;
                        if (UnityEngine.Input.GetMouseButtonUp(mouseNumber) && state == EButtonState.Up)
                            return true;
                        break;
                    default: 
                            if (state == EButtonState.Down && device.GetButtonDown(ToVRButton(eButtonType))) return true;
                            if (state == EButtonState.Pressed && device.GetButton(ToVRButton(eButtonType))) return true;
                            if (state == EButtonState.Up && device.GetButtonUp(ToVRButton(eButtonType))) return true;
                        break;
                }
            }

            return false;
        }

        public int GetMouseNumber(EButtonType type)
        {
            var mouseNumber = 0;

            switch (type)
            {
                case EButtonType.LeftMouse:
                    mouseNumber = 0;
                    break;
                case EButtonType.MiddleMouse:
                    mouseNumber = 2;
                    break;
                case EButtonType.RightMouse:
                    mouseNumber = 1;
                    break;
            }

            return mouseNumber;
        }

        public string ToVRButton(EButtonType type)
        {
            switch (type)
            {
                case EButtonType.One:
                    return VRButton.One;
                case EButtonType.Two:
                    return VRButton.Two;
                case EButtonType.Three:
                    return VRButton.Three;
                case EButtonType.Four:
                    return VRButton.Four;
                case EButtonType.Trigger:
                    return VRButton.Trigger;
                case EButtonType.Touch:
                    return VRButton.Touch;
                case EButtonType.Back:
                    return VRButton.Back;
                default:
                    return "None";
            }
        }
    }
}