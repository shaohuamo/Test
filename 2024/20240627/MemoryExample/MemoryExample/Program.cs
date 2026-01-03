namespace MemoryExample
{
    public class NetworkBuffer
    {
        public Memory<byte> _buffer;

        public NetworkBuffer(int size)
        {
            _buffer = new byte[size];
        }

        public void FillBuffer(byte[] data)
        {
            if (data.Length > _buffer.Length)
                throw new ArgumentException("Data too large for buffer");

            data.CopyTo(_buffer);
        }

        public Memory<byte> GetBuffer()
        {
            return _buffer;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var buffer = new NetworkBuffer(1024);
            Console.WriteLine(buffer._buffer.Length);
            var array = new byte[] { 1, 2, 3, 4 };
            buffer.FillBuffer(array);
            Memory<byte> data = buffer.GetBuffer();
            data.Span[0] = 5;

            Console.WriteLine(array[0]);

            Span<string> a = Span<string>.Empty;

        }
    }
}
