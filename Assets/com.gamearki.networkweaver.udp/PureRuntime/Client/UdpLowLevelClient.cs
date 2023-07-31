using System;
using LiteNetLib;

namespace GameArki.Network {

    public class UdpLowLevelClient {

        EventBasedNetListener listener;
        NetManager client;

        NetPeer remotePeer;
        string host;
        int port;
        string key;

        NetworkConnectionType connectionType;
        public NetworkConnectionType ConnectionType => connectionType;

        public event Action OnConnectedHandle;
        public event Action OnDisconnectedHandle;
        public event Action<byte[]> OnRecvDataHandle;

        public UdpLowLevelClient() {
            this.listener = new EventBasedNetListener();
            this.client = new NetManager(listener);

            client.UpdateTime = 15;

            listener.PeerConnectedEvent += OnConnected;
            listener.PeerDisconnectedEvent += OnDisconnected;
            listener.NetworkReceiveEvent += OnRecv;

            client.Start();
        }

        public void Connect(string host, int port, string key) {
            this.host = host;
            this.port = port;
            this.key = key;

            if (connectionType != NetworkConnectionType.Reconnecting) {
                connectionType = NetworkConnectionType.Connecting;
            }
            remotePeer = client.Connect(host, port, key);
        }

        public void Tick() {
            this.client.PollEvents();
        }

        void OnConnected(NetPeer peer) {
            this.remotePeer = peer;
            OnConnectedHandle.Invoke();
            connectionType = NetworkConnectionType.Connected;
        }

        public void Reconnect() {
            Connect(host, port, key);
            connectionType = NetworkConnectionType.Reconnecting;
        }

        void OnDisconnected(NetPeer peer, DisconnectInfo disconnectInfo) {
            this.remotePeer = null;
            OnDisconnectedHandle.Invoke();
            connectionType = NetworkConnectionType.Disconnected;
        }

        void OnRecv(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod) {
            byte[] data = reader.GetRemainingBytes();
            OnRecvDataHandle.Invoke(data);
        }

        public void Send(byte[] data) {
            remotePeer.Send(data, DeliveryMethod.ReliableSequenced);
        }

        public void Stop() {
            client.Stop();
            connectionType = NetworkConnectionType.None;
        }

    }

}