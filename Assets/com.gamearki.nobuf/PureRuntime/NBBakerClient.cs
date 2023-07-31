using System;
using System.Collections.Generic;

namespace GameArki.NoBuf {

    public class NBBakerClient {

        Stack<byte[]> _pool;
        int _messageSize;

        Dictionary<ushort, Func<INoBufMessage>> listenedMessages;

        Dictionary<ushort, Action<INoBufMessage>> cliListens;

        public NBBakerClient() { }

        public void Initialize(int poolSize, int messageSize) {
            _pool = new Stack<byte[]>(poolSize);
            for (int i = 0; i < poolSize; i++) {
                _pool.Push(new byte[messageSize]);
            }
            _messageSize = messageSize;

            listenedMessages = new Dictionary<ushort, Func<INoBufMessage>>(256);
            cliListens = new Dictionary<ushort, Action<INoBufMessage>>(256);
        }

        // - Listen
        public void Register<T>(byte serviceID, byte messageID, Func<INoBufMessage> messageFactory, Action<T> action) where T : INoBufMessage {
            Register(serviceID, messageID, messageFactory, (INoBufMessage msg) => {
                action.Invoke((T)msg);
            });
        }

        void Register(byte serviceID, byte messageID, Func<INoBufMessage> messageFactory, Action<INoBufMessage> action) {
            ushort key = (ushort)(serviceID | (messageID << 8));
            listenedMessages.Add(key, messageFactory);
            cliListens.Add(key, action);
        }

        public void Trigger(byte[] src) {
            int offset = 0;
            ushort key = NBReader.R_U16(src, ref offset);
            if (listenedMessages.TryGetValue(key, out Func<INoBufMessage> messageFactory)
                    && cliListens.TryGetValue(key, out Action<INoBufMessage> action)) {
                INoBufMessage msg = messageFactory.Invoke();
                msg.FromBytes(src, ref offset);
                action.Invoke(msg);
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