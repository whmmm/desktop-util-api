using System.Net.WebSockets;
using System.Text;
using tauri_api.Domain.Beans.Message;
using Newtonsoft.Json;
using tauri_api.Core.util;

namespace tauri_api.Service;

public class SocketMessageService
{
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="socket">连接</param>
    /// <param name="text">文本</param>
    public async void sendMessage(WebSocket socket,
                                  MessageType messageType,
                                  string? text)
    {
        if (text == null)
        {
            text = "";
        }

        MessageDto dto = new MessageDto()
        {
            MessageType = messageType,
            Message = text
        };
        string json = JsonUtil.ToJson(dto);

        byte[] buffer = Encoding.UTF8.GetBytes(json);


        ArraySegment<byte> segment = new(buffer);
        socket.SendAsync(segment,
                         WebSocketMessageType.Text,
                         true,
                         CancellationToken.None);
    }
}