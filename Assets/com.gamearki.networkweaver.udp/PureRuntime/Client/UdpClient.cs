using System;
using System.Collections.Generic;
using GameArki.BufferIO;

namespace GameArki.Network {

    public class UdpClient {

        UdpLowLevelClient client;
        IProtocolService protocolService;

        Dictionary<ushort, Action<ArraySegment<byte>>> dic;

        public NetworkConnectionType ConnectionType => client.ConnectionType;

        public event Action OnConnectedHandle;
        public event Action OnDisconnectedHandle;

        public UdpClient() {
            client = new UdpLowLevelClient();
            dic = new Dictionary<ushort, Action<ArraySegment<byte>>>();

            client.OnConnectedHandle += OnConnected;
            client.OnRecvDataHandle += OnRecvData;
            client.OnDisconnectedHandle += OnDisconnected;
        }

        public void Inject(IProtocolService protocolService) {
            this.protocolService = protocolService;
        }

        public void Tick() {
            client.Tick();
        }

        public void Connect(string host, int port) {
            client.Connect(host, port, "");
        }

        public void Reconnect() {
            client.Reconnect();
        }

        public void Disconnect() {
            client.Stop();
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
            key |= (ushort)((ushort)messageId << 8);

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

        void OnRecvData(byte[] data) {
            if (data.Length < 2) {
                return;
            }
            byte serviceId = data[0];
            byte messageId = data[1];
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