using System;

namespace GameArki.NoBuf {

    public interface IProtocolService {

        (byte serviceID, byte messageID) GetMessageID(Type type);
        Func<INoBufMessage> GetGenerateHandle(Type type);

    }

}