/// <summary>
/// The root of a scene module menu. 
/// </summary>
public class ModuleMenuSceneRoot : SceneRoot<ModuleMenuSceneState>
{
    public void ModuleSelect(ModuleData moduleData)
    {
        Data.StartModule(moduleData, moduleData.ModuleType);
        Done();
    }
}
