using System;
using System.Collections.Generic;
using LiteNetLib;

namespace GameArki.Network {

    public class UdpLowLevelServer {

        EventBasedNetListener listener;
        NetManager server;

        int port;
        public int Port => port;

        ServerConnectionType connectionType;
        public ServerConnectionType ConnectionType => connectionType;

        SortedDictionary<int, NetPeer> all;

        public event Action<int> OnConnectedHandle;
        public event Action<int, byte[]> OnRecvDataHandle;
        public event Action<int> OnDisconnectedHandle;

        public UdpLowLevelServer() {

            this.all = new SortedDictionary<int, NetPeer>();

            this.listener = new EventBasedNetListener();
            this.server = new NetManager(listener);

            listener.ConnectionRequestEvent += OnConnectionRequest;
            listener.PeerConnectedEvent += OnPeerConnected;
            listener.PeerDisconnectedEvent += OnPeerDisconnected;
            listener.NetworkReceiveEvent += OnRecvData;

            server.UpdateTime = 15;

        }

        public void StartListen(int port) {
            this.port = port;
            bool isSuc = this.server.Start(port);
            if (isSuc) {
                connectionType = ServerConnectionType.Running;
            } else {
                connectionType = ServerConnectionType.ListenFailed;
            }
        }

        public void Tick() {
            this.server.PollEvents();
        }

        public void Stop() {
            this.server.Stop();
            connectionType = ServerConnectionType.None;
        }

        void OnConnectionRequest(ConnectionRequest request) {
            request.Accept();
        }

        void OnPeerConnected(NetPeer peer) {
            all.TryAdd(peer.Id, peer);
            OnConnectedHandle.Invoke(peer.Id);
        }

        void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo) {
            all.Remove(peer.Id);
            OnDisconnectedHandle.Invoke(peer.Id);
        }

        void OnRecvData(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod) {
            byte[] data = reader.GetRemainingBytes();
            OnRecvDataHandle.Invoke(peer.Id, data);
        }

        public void Send(int connID, byte[] data, DeliveryMethod method = DeliveryMethod.ReliableOrdered) {
            bool hasPeer = all.TryGetValue(connID, out var peer);
            if (hasPeer) {
                peer.Send(data, method);
            }
            
        }

    }

}