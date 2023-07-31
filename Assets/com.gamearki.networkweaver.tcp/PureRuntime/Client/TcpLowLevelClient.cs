using System;
using Telepathy;

namespace GameArki.Network {

    public class TcpLowLevelClient {

        Client client;
        string host;
        int port;
        NetworkConnectionType connectionType;

        public string Host => host;
        public int Port => port;
        public NetworkConnectionType ConnectionType => connectionType;

        public event Action OnConnectedHandle;
        public event Action<ArraySegment<byte>> OnDataHandle;
        public event Action OnDisconnectedHandle;

        public TcpLowLevelClient() {}

        public void Init(int maxMessageSize) {
            client = new Client(maxMessageSize);
            client.OnConnected += OnConnected;
            client.OnData += OnData;
            client.OnDisconnected += OnDisconnected;
            connectionType = NetworkConnectionType.Disconnected;
        }

        public bool IsConnected() {
            return client.Connected;
        }

        public void Connect(string host, int port) {
            this.host = host;
            this.port = port;
            client.Connect(host, port);
            if (connectionType != NetworkConnectionType.Reconnecting) {
                connectionType = NetworkConnectionType.Connecting;
            }
        }

        public void Reconnect() {
            if (IsConnected()) {
                client.Disconnect();
            }
            connectionType = NetworkConnectionType.Reconnecting;
            Connect(host, port);
        }

        public void Disconnect() {
            client.Disconnect();
            connectionType = NetworkConnectionType.Disconnected;
        }

        public void Tick(int processLimit = 100) {
            client.Tick(processLimit);
        }

        public bool Send(ArraySegment<byte> data) {
            return client.Send(data);
        }

        void OnConnected() {
            if (OnConnectedHandle != null) {
                OnConnectedHandle.Invoke();
                connectionType = NetworkConnectionType.Connected;
            }
        }

        void OnData(ArraySegment<byte> data) {
            if (OnDataHandle != null) {
                OnDataHandle.Invoke(data);
            }
        }

        void OnDisconnected() {
            if (OnDisconnectedHandle != null) {
                OnDisconnectedHandle.Invoke();
                if (connectionType == NetworkConnectionType.Connecting) {
                    connectionType = NetworkConnectionType.ConnectFailed;
                } else if (connectionType == NetworkConnectionType.Reconnecting) {
                    connectionType = NetworkConnectionType.ReconnectFailed;
                }
            } else {
                throw new Exception("未注册: " + nameof(OnDisconnectedHandle));
            }
        }

    }

}