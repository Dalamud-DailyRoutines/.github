using Dalamud.Interface;

namespace DailyRoutines.Modules;

public class Sample : DailyModuleBase
{
    public override ModuleInfo Info => new()
    {
        Title = "",
        Description = "",
        Category = ModuleCategories.General,
        Author = [""],
    };

    private static Config ModuleConfig = null!;

    public class Config : ModuleConfiguration
    {
        /*
        Variables that can be serialized
        */
    }

    static Sample() { }

    public override void Init() { ModuleConfig = new Config().Load(this); }

    public override void Uninit() { }

    public override void ConfigUI() { }
}
