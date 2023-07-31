namespace GameArki.NoBuf {

    public interface INoBufMessage {

        void WriteTo(byte[] dst, ref int offset);
        void FromBytes(byte[] src, ref int offset);
        void Dispose();

    }

}