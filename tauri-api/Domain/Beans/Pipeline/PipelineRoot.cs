namespace tauri_api.Domain.Beans.Pipeline;

public class PipelineRoot
{
    public BuildInfo? Build { set; get; }

    public List<DeployInfo>? Deploy { set; get; }
}