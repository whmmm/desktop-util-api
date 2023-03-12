using System.Net.WebSockets;
using Microsoft.AspNetCore.WebUtilities;
using SqlSugar;
using tauri_api.Core;
using tauri_api.Core.util;
using tauri_api.Domain.Beans.Command;

namespace tauri_api.Service;

/// <summary>
/// 处理命令
/// </summary>
public class CommandService
{
    private readonly byte[] _buffer = new byte[1024 * 4];
    private PipelineService _pipelineService;
    private ProjectService _projectService;

    public CommandService(PipelineService pipelineService,
                          ProjectService projectService)
    {
        _pipelineService = pipelineService;
        _projectService = projectService;
    }

    public virtual async Task<WebSocketReceiveResult> handleMessage(WebSocket ws)
    {
        var result = await ws.ReceiveAsync(
                                           new ArraySegment<byte>(_buffer), CancellationToken.None);

        var str = System.Text.Encoding.Default.GetString(_buffer, 0, result.Count);
        var commandInfo = this.ParseCommand(str);
        if (commandInfo == null)
        {
            throw new Exception("发送的数据不是命令, 断开连接");
        }

        switch (commandInfo.Type)
        {
            case CommandType.Start:
                Start(commandInfo, ws);
                break;
            case CommandType.End:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return result;
    }

    /// <summary>
    /// 启动执行 pipeline
    /// </summary>
    /// <param name="commandInfo">命令信息</param>
    /// <param name="websocket">websocket, 用于发送日志</param>
    /// <exception cref="NullReferenceException"></exception>
    private void Start(CommandInfo commandInfo, WebSocket websocket)
    {
        var projectEntity = _projectService.GetById(commandInfo.ProjectId);
        var pipeline = _pipelineService.ParseFile(
                                                  _pipelineService.GetPath(projectEntity.DirPath)
                                                 );
        if (pipeline == null)
        {
            throw new NullReferenceException("pipeline 不能为空");
        }

        var build = pipeline.Build;
        if (build != null)
        {
            _pipelineService.ExecBuild(build, projectEntity, websocket);
        }
    }

    private CommandInfo? ParseCommand(string commandString)
    {
        if (commandString.StartsWith("command://"))
        {
            commandString = commandString.Replace("command://", "http://host?");
            var uri = new Uri(commandString);
            var query = QueryHelpers.ParseQuery(uri.Query);

            var info = new CommandInfo();

            info.Type = EnumUtil.Parse<CommandType>(query["command"]);
            info.ProjectId = int.Parse(query["projectId"].ToString());

            return info;
        }
        else
        {
            return null;
        }
    }
}