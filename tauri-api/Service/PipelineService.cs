using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using SharpYaml.Serialization;
using tauri_api.Domain.Beans.Message;
using tauri_api.Domain.Beans.Pipeline;
using tauri_api.Domain.Entity;

namespace tauri_api.Service;

/// <summary>
/// 解析 yml
/// </summary>
public class PipelineService
{
    private SocketMessageService _socketMessageService;

    public PipelineService(SocketMessageService socketMessageService)
    {
        _socketMessageService = socketMessageService;
    }

    /// <summary>
    /// 解析 pipeline 文件
    /// </summary>
    /// <param name="path">目录地址</param>
    public PipelineRoot? ParseFile(string path)
    {
        var serializer = new Serializer();
        var text = File.ReadAllText(path);
        PipelineRoot? pipelineRoot = null;
        try
        {
            pipelineRoot = serializer.Deserialize<PipelineRoot>(text);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return pipelineRoot;
    }

    /// <summary>
    /// 是否含有编译文件
    /// </summary>
    /// <param name="folder"></param>
    /// <returns></returns>
    public bool HasFile(string folder)
    {
        var exists = Path.Exists(folder + "/" + "_deploy.yml");

        return exists;
    }

    public string GetPath(string dirPath)
    {
        return dirPath + "/" + "_deploy.yml";
    }

    /// <summary>
    /// 执行 build
    /// </summary>
    /// <param name="build">构建信息</param>
    /// <param name="project">相关的执行目录</param>
    /// <param name="websocket">websocket, 用于输出日志</param>
    public void ExecBuild(BuildInfo build,
                          ProjectEntity project,
                          WebSocket websocket)
    {
        string path = project.DirPath;

        foreach (string cmd in build.Cmd)
        {
            int firstSpace = cmd.IndexOf(' ');
            string exe = cmd.Substring(0, firstSpace);
            string args = cmd.Substring(firstSpace).Trim();
            // string? variable = Environment.GetEnvironmentVariable("MVN_HOME");
            if (exe == "mvn" || exe == "mvn.cmd")
            {
                exe = Environment.GetEnvironmentVariable("MVN_HOME") + "/bin/" + exe;
            }

            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = exe,
                    Arguments = args,
                    CreateNoWindow = true,
                    Environment =
                    {
                        { "PATH", System.Environment.GetEnvironmentVariable("PATH") }
                    },
                    WorkingDirectory = path,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                },
            };
            bool start = process.Start();

            this.HandleOutput(process.StandardOutput, websocket);

            // string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();
        }
    }

    private async void HandleOutput(StreamReader processStandardOutput,
                                    WebSocket socket)
    {
        while (!processStandardOutput.EndOfStream)
        {
            string? readLine = processStandardOutput.ReadLine();

            _socketMessageService.sendMessage(socket,
                                              MessageType.Info,
                                              readLine
                                             );
        }
    }
}