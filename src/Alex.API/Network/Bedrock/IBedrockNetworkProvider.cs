using Alex.API.Data;
using Alex.API.World;
using MiNET.Net;

namespace Alex.API.Network.Bedrock
{
    public interface IBedrockNetworkProvider : INetworkProvider
    {
        IWorldReceiver WorldReceiver { get; set; }

        void SendPacket(Packet packet);

        void RequestChunkRadius(int radius);
        void SendDisconnectionNotification();
        void SendMcpeMovePlayer();

        void InitiateEncryption(byte[] x5u, byte[] salt);

        void ShowDisconnect(string reason, bool useTranslation = false);
    }
}