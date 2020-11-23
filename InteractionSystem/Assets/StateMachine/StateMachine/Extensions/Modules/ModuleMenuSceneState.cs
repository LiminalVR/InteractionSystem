using System;
using System.Collections.Generic;
using App.StateMachine;
using App.StateMachine.Examples;

/// <summary>
/// The state that opens a menu scene and then begins a module.
/// </summary>
public class ModuleMenuSceneState : SceneState<ModuleMenuSceneState>
{
    public SequenceStateMachine SequenceStateMachine;

    public ModuleDictionary Modules = new ModuleDictionary();

    public void StartModule(ModuleData moduleData, ModuleType moduleType)
    {
        var module = Modules[moduleType];
        module.SceneReference = moduleData.SceneReference;
        SequenceStateMachine.Add(module);
    }
}

[Serializable]
public class ModuleDictionary : SerializableDictionary<ModuleType, SceneState> { }