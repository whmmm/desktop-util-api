using NUnit.Framework;
using tauri_api.Service;

namespace tauri_api.Test;

[TestFixture]
public class PipelineServiceTest
{
    private readonly PipelineService _pipelineService;

    public PipelineServiceTest(PipelineService pipelineService)
    {
        _pipelineService = pipelineService;
    }

    [Test]
    public void TestPipeline()
    {
        var path = "C:\\data\\Rider\\tauri-api\\tauri-api\\tauri-api\\Test\\test.yml";
        _pipelineService.ParseFile(path);
    }
}