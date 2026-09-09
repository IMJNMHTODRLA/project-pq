using System.Threading.Tasks;
using Godot;
using ProjectPQ.addons.localizers.Maps;

namespace ProjectPQ.Scripts;

public sealed partial class SettingManager : Singleton<SettingManager>
{
    public bool IsLoad => InitTask.IsCompletedSuccessfully;

    private readonly TaskCompletionSource<bool> _initTcs = new();
    public Task InitTask => _initTcs.Task;

    public string SetLangId = null!;

    protected override void OnAutoload()
    {
        _ = SafeUtils.RunAsync(
            Initialize,
            (e) =>
            {
                _initTcs.TrySetResult(false);
                GD.PrintErr(e);
            }
        );
    }

    protected override void OnExit()
    {
        _ = SafeUtils.RunAsync(
            async () =>
                await AppDataUtils.WriteTextAsync("options/lang", SetLangId),
            (e) => GD.PrintErr(e)
        );
    }

    private async Task Initialize()
    {
        SetLangId = await AppDataUtils.ReadTextAsync("options/lang", LangId.EN);
        TranslationServer.SetLocale(SetLangId);

        _initTcs.TrySetResult(true);
    }

    protected override void OnTick(double delta)
    {
    }
}