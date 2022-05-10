using Modding.Utils;
using Object = UnityEngine.Object;

namespace GravityMod;

public class GravityMod : Mod, ITogglableMod
{
    public override string GetVersion() => "1.0.0.1";

    public static GravityMod Instance;
    private int count;

    public override void Initialize()
    {
        Instance ??= this;
        ModHooks.HeroUpdateHook += AddGravityToAllGameObjects;
    }
    
    private void AddGravityToAllGameObjects()
    {
        count++;
        if (count > 30)
        {
            count = 0;
            foreach (var go in Object.FindObjectsOfType<Rigidbody2D>()
                         .Select(rb2d => rb2d.gameObject)
                         .Where(go => go.name != HeroController.instance.gameObject.name))
            {
                go.gameObject.GetOrAddComponent<GravityComponent>();
            }
        }
    }


    public void Unload()
    {
        ModHooks.HeroUpdateHook -= AddGravityToAllGameObjects;
    }

}