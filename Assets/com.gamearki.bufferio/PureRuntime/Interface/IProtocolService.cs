using System;

namespace GameArki.BufferIO {

    public interface IProtocolService {

        (byte serviceID, byte messageID) GetMessageID<T>() where T : IBufferIOMessage<T>;
        Func<T> GetGenerateHandle<T>() where T : IBufferIOMessage<T>;

    }

}