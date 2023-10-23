using System;
using System.Collections.Generic;
using GameArki.BufferIO;

namespace GameArki.Network {

    public class TcpServer {

        TcpLowLevelServer server;
        int maxMessageSize;
        IProtocolService protocolService;
        Pool<byte[]> pool;

        Dictionary<ushort, Action<int, ArraySegment<byte>>> dic;

        public event Action<int> OnConnectedHandle;
        public event Action<int> OnDisconnectedHandle;

        public TcpServer() {
            server = new TcpLowLevelServer();
            dic = new Dictionary<ushort, Action<int, ArraySegment<byte>>>();
            pool = new Pool<byte[]>();
        }

        public void Inject(IProtocolService protocolService) {
            this.protocolService = protocolService;
        }

        public void Init(int maxMessageSize = 4096) {
            this.maxMessageSize = maxMessageSize;
            server.Init(maxMessageSize);

            server.OnConnectedHandle += OnConnected;
            server.OnDisconnectedHandle += OnDisconnected;
            server.OnDataHandle += OnRecvData;

            pool.SetGenerator(() => new byte[maxMessageSize]);
        }

        public void Tick() {
            server.Tick();
        }

        public void StartListen(int port) {
            server.StartListen(port);
        }

        public void RestartListen() {
            server.RestartListen();
        }

        public void StopListen() {
            server.StopListen();
        }

        public void Send<T>(int connID, T msg) where T : IBufferIOMessage<T> {
            (byte serviceId, byte messageId) = protocolService.GetMessageID<T>();
            Send<T>(serviceId, messageId, connID, msg);
        }

        public void On<T>(Action<int, T> handle) where T : IBufferIOMessage<T> {
            (byte serviceId, byte messageId) = protocolService.GetMessageID<T>();
            On<T>(serviceId, messageId, protocolService.GetGenerateHandle<T>(), handle);
        }

        public void Send<T>(byte serviceId, byte messageId, int connId, T msg) where T : IBufferIOMessage<T> {
            byte[] buffer = pool.Take();
            int offset = 0;
            buffer[offset++] = serviceId;
            buffer[offset++] = messageId;
            msg.WriteTo(buffer, ref offset);
            if (offset >= maxMessageSize - 2) {
                throw new Exception("Message is too long");
            }
            server.Send(connId, new ArraySegment<byte>(buffer, 0, offset));
            pool.Return(buffer);
        }

        public void On<T>(byte serviceId, byte messageId, Func<T> generateHandle, Action<int, T> handle) where T : IBufferIOMessage<T> {

            if (generateHandle == null) {
                throw new Exception("未注册: " + nameof(generateHandle));
            }

            ushort key = (ushort)serviceId;
            key |= (ushort)(messageId << 8);
            if (!dic.ContainsKey(key)) {
                dic.Add(key, (connId, byteData) => {
                    T msg = generateHandle.Invoke();
                    int offset = 2;
                    msg.FromBytes(byteData.Array, ref offset);
                    handle.Invoke(connId, msg);
                });
            }

        }

        void OnConnected(int connId) {
            if (OnConnectedHandle != null) {
                OnConnectedHandle.Invoke(connId);
            }
        }

        void OnDisconnected(int connId) {
            if (OnDisconnectedHandle != null) {
                OnDisconnectedHandle.Invoke(connId);
            }
        }

        void OnRecvData(int connId, ArraySegment<byte> data) {
            var arr = data.Array;
            if (arr.Length < 2) {
                return;
            }

            byte serviceId = arr[0];
            byte messageId = arr[1];
            ushort key = (ushort)serviceId;
            key |= (ushort)(messageId << 8);
            dic.TryGetValue(key, out var handle);
            if (handle != null) {
                handle.Invoke(connId, data);
            } else {
                System.Console.WriteLine($"未注册 serviceId:{serviceId}, messageId:{messageId}");
            }

        }

    }

}