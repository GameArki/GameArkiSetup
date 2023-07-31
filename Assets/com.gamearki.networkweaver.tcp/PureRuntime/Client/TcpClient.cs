using System;
using System.Collections.Generic;
using GameArki.BufferIO;

namespace GameArki.Network {

    public class TcpClient {

        TcpLowLevelClient client;
        IProtocolService protocolService;

        public string Host => client.Host;
        public int Port => client.Port;
        public NetworkConnectionType ConnectionType => client.ConnectionType;

        Dictionary<ushort, Action<ArraySegment<byte>>> dic;

        public event Action OnConnectedHandle;
        public event Action OnDisconnectedHandle;

        public TcpClient() {
            client = new TcpLowLevelClient();
            dic = new Dictionary<ushort, Action<ArraySegment<byte>>>();
        }

        public void Inject(IProtocolService protocolService) {
            this.protocolService = protocolService;
        }

        public void Init(int maxMessageSize = 1024) {
            client.Init(maxMessageSize);

            client.OnConnectedHandle += OnConnected;
            client.OnDataHandle += OnRecvData;
            client.OnDisconnectedHandle += OnDisconnected;
        }

        public void Tick(int processLimit = 100) {
            client.Tick(processLimit);
        }

        public bool IsConnected() {
            return client.IsConnected();
        }

        public void Connect(string host, int port) {
            client.Connect(host, port);
        }

        public void Reconnect() {
            client.Reconnect();
        }

        public void Disconnect() {
            client.Disconnect();
        }

        public void Send<T>(T msg) where T : IBufferIOMessage<T> {
            (byte serviceId, byte messageId) = protocolService.GetMessageID<T>();
            Send<T>(serviceId, messageId, msg);
        }

        public void On<T>(Action<T> handle) where T : IBufferIOMessage<T> {
            (byte serviceId, byte messageId) = protocolService.GetMessageID<T>();
            On<T>(serviceId, messageId, protocolService.GetGenerateHandle<T>(), handle);
        }

        public void Send<T>(byte serviceId, byte messageId, T msg) where T : IBufferIOMessage<T> {
            byte[] buffer = msg.ToBytes();
            byte[] dst = new byte[buffer.Length + 2];
            int offset = 0;
            dst[offset] = serviceId;
            offset += 1;
            dst[offset] = messageId;
            offset += 1;
            Buffer.BlockCopy(buffer, 0, dst, offset, buffer.Length);
            client.Send(dst);
        }

        public void On<T>(byte serviceId, byte messageId, Func<T> generateHandle, Action<T> handle) where T : IBufferIOMessage<T> {

            if (generateHandle == null) {
                throw new Exception($"未注册: " + nameof(generateHandle));
            }

            ushort key = (ushort)serviceId;
            key |= (ushort)(messageId << 8);

            if (!dic.ContainsKey(key)) {
                dic.Add(key, (byteData) => {
                    T msg = generateHandle.Invoke();
                    int offset = 2;
                    msg.FromBytes(byteData.Array, ref offset);
                    handle.Invoke(msg);
                });
            }
        }

        void OnConnected() {
            if (OnConnectedHandle != null) {
                OnConnectedHandle.Invoke();
            }
        }

        void OnDisconnected() {
            if (OnDisconnectedHandle != null) {
                OnDisconnectedHandle.Invoke();
            }
        }

        void OnRecvData(ArraySegment<byte> data) {
            var arr = data.Array;
            if (arr.Length < 2) {
                throw new Exception($"消息长度过短: {arr.Length}");
            }
            byte serviceId = arr[0];
            byte messageId = arr[1];
            ushort key = (ushort)serviceId;
            key |= (ushort)(messageId << 8);
            dic.TryGetValue(key, out var handle);
            if (handle != null) {
                handle.Invoke(data);
            } else {
                System.Console.WriteLine($"未注册 serviceId:{serviceId}, messageId:{messageId}");
            }
        }

    }

}