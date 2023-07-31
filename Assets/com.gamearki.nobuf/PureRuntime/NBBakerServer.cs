using System;
using System.Collections.Generic;

namespace GameArki.NoBuf {

    public class NBBakerServer {

        Stack<byte[]> _pool;
        int _messageSize;

        Dictionary<ushort, Func<INoBufMessage>> listenedMessages;

        Dictionary<ushort, Action<int, INoBufMessage>> svrListens;

        public NBBakerServer() { }

        public void Initialize(int poolSize, int messageSize) {
            _pool = new Stack<byte[]>(poolSize);
            for (int i = 0; i < poolSize; i++) {
                _pool.Push(new byte[messageSize]);
            }
            _messageSize = messageSize;

            listenedMessages = new Dictionary<ushort, Func<INoBufMessage>>(256);
            svrListens = new Dictionary<ushort, Action<int, INoBufMessage>>(256);
        }

        // - Listen
        public void Register<T>(byte serviceID, byte messageID, Func<INoBufMessage> messageFactory, Action<int, T> action) where T : INoBufMessage {
            Register(serviceID, messageID, messageFactory, (int connID, INoBufMessage msg) => {
                action.Invoke(connID, (T)msg);
            });
        }

        void Register(byte serviceID, byte messageID, Func<INoBufMessage> messageFactory, Action<int, INoBufMessage> action) {
            ushort key = (ushort)(serviceID | (messageID << 8));
            listenedMessages.Add(key, messageFactory);
            svrListens.Add(key, action);
        }

        public void Trigger(int connID, byte[] src) {
            int offset = 0;
            ushort key = NBReader.R_U16(src, ref offset);
            if (listenedMessages.TryGetValue(key, out Func<INoBufMessage> messageFactory)
                    && svrListens.TryGetValue(key, out Action<int, INoBufMessage> action)) {
                INoBufMessage msg = messageFactory.Invoke();
                msg.FromBytes(src, ref offset);
                action.Invoke(connID, msg);
                msg.Dispose();
            } else {
                throw new Exception($"NoBuf message not listened: {key}");
            }
        }

        // - Bake
        public ArraySegment<byte> Bake_Take(byte serviceID, byte messageID, INoBufMessage msg) {
            byte[] buffer = Take();
            int offset = 0;
            ushort key = (ushort)(serviceID | (messageID << 8));
            NBWriter.W_U16(buffer, key, ref offset);
            msg.WriteTo(buffer, ref offset);
            return new ArraySegment<byte>(buffer, 0, offset);
        }

        byte[] Take() {
            byte[] buffer;
            if (_pool.Count != 0) {
                buffer = _pool.Pop();
            } else {
                buffer = new byte[_messageSize];
                _pool.Push(buffer);
            }
            return buffer;
        }

        public void Bake_ReturnAfterSend(byte[] buffer) {
            _pool.Push(buffer);
        }

    }
}