#if DEBUG
using DailyRoutines.Managers;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Plugin.Services;

namespace DailyRoutines.Modules;

public class AutoRetarget : DailyModuleBase
{
    private static string displayName = "无";
    public override ModuleInfo Info => new()
    {
        Title = "自动选中目标",
        Description = "没有目标的时候会自动尝试选中目标玩家（写来和人贴贴用的玩具，一直盯着（x））",
        Category = ModuleCategories.General,
        Author = ["KirisameVanilla"],
    };

    public override void ConfigUI()
    {
        ImGui.InputText("Name@World##input", ref displayName, 64);
        if (ImGui.Button("Set"))
        {
            if (DService.Targets.Target is not IPlayerCharacter ipc) displayName = $"{DService.Targets.Target?.Name}";
            else
                displayName =
                $"{DService.Targets.Target?.Name}@{((IPlayerCharacter)DService.Targets.Target).HomeWorld.GameData.Name}";
        }
        ImGui.SameLine();
        if (ImGui.Button("Clear"))
        {
            displayName = "无";
        }
    }

    public override void Init()
    {
        Service.FrameworkManager.Register(true, OnUpdate);
    }

    private static void OnUpdate(IFramework framework)
    {
        if (displayName == "无") return;

        if (DService.Targets.Target == null)
        {
            foreach (var igo in DService.ObjectTable)
            {
                string objName;
                if (igo is not IPlayerCharacter ipc) objName = igo.Name.ToString();
                else objName = $"{igo.Name}@{ipc.HomeWorld.GameData.Name}";
                if (objName!=displayName) continue;
                DService.Targets.Target = igo;
                return;
            }
        }
    }
}
#endif
