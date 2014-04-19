using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SweNet
{

    /// <summary>
    /// File C
    /// </summary>
    public class CFile : IDisposable
    {
        private Stream _Stream;
        private Encoding _Encoding;
        private Queue<byte> _ReadBuffer;

        /// <summary>
        /// Create new C file access
        /// </summary>
        public CFile(Stream stream, Encoding encoding = null) {
            _ReadBuffer = new Queue<byte>();
            this._Stream = stream;
            EOF = _Stream == null;
            this._Encoding = encoding ?? Encoding.GetEncoding("Windows-1252");
        }

        /// <summary>
        /// Internal release resources
        /// </summary>
        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                if (_Stream != null) {
                    _Stream.Dispose();
                    _Stream = null;
                }
                _Encoding = null;
            }
        }        

        /// <summary>
        /// Release resource
        /// </summary>
        public void Dispose() {
            Dispose(true);
        }

        /// <summary>
        /// Verify if the file can read another byte
        /// </summary>
        void CheckEOF() {
            if (EOF && _ReadBuffer.Count == 0)
                throw new EndOfStreamException();
        }

        /// <summary>
        /// Verify if a byte is a EOF mark
        /// </summary>
        /// <param name="c"></param>
        void CheckEOF(int c) {
            if(c<0)
                throw new EndOfStreamException();
        }

        /// <summary>
        /// Seek the file
        /// </summary>
        public long Seek(long offset, SeekOrigin origin) {
            return _Stream.Seek(offset, origin);
        }

        /// <summary>
        /// Read the next byte
        /// </summary>
        int ReadNextByte() {
            if (_ReadBuffer.Count > 0)
                return _ReadBuffer.Dequeue();
            if (EOF) return -1;
            var res = _Stream.ReadByte();
            if (res < 0) {
                EOF = true;
            }
            return res;
        }

        /// <summary>
        /// Read an array of bytes
        /// </summary>
        int ReadBytes(byte[] bytes, int count) {
            return ReadBytes(bytes, 0, count);
        }

        /// <summary>
        /// Read an array of bytes
        /// </summary>
        int ReadBytes(byte[] bytes, int offset, int count) {
            if (bytes == null) return 0;
            int offInit = offset;
            while (_ReadBuffer.Count > 0 && count > 0) {
                bytes[offset++] = _ReadBuffer.Dequeue();
                count--;
            }
            return (offset - offInit) + _Stream.Read(bytes, offset, count);
        }

        /// <summary>
        /// Read a line of text
        /// </summary>
        /// <remarks>
        /// </remarks>
        public string ReadLine() {
            if (_Stream == null) return null;
            List<byte> buff = new List<byte>();
            int rb;
            while ((rb = ReadNextByte()) > 0) {
                byte c = (byte)rb;
                // Check if end of line
                bool endOfLine = false;
                if (c == '\r') {
                    endOfLine = true;
                    rb = ReadNextByte();
                    c = (byte)rb;
                }
                if (c == '\n') {
                    endOfLine = true;
                } else if (endOfLine) {
                    _ReadBuffer.Enqueue(c);
                }
                if (endOfLine) break;
                //
                buff.Add(c);
            }
            if (EOF && buff.Count == 0) return null;
            return _Encoding.GetString(buff.ToArray(), 0, buff.Count);
        }

        /// <summary>
        /// Read an encoded char
        /// </summary>
        public Char ReadChar() {
            List<byte> bytes = new List<byte>();
            do {
                int b = ReadNextByte();
                CheckEOF(b);
                bytes.Add((byte)b);
                // TODO Test if this work with UTF-8 encoded char
                var result = _Encoding.GetChars(bytes.ToArray());
                if (result.Length > 0) return result[0];
            } while (true);
        }

        /// <summary>
        /// Read an array of chars
        /// </summary>
        public Char[] ReadChars(int count) {
            var result = new Char[count];
            for (int i = 0; i < count; i++) {
                result[i] = ReadChar();
            }
            return result;
        }

        /// <summary>
        /// Read a string
        /// </summary>
        public bool ReadString(ref string s, int size) {
            var chars = ReadChars(size);
            if (chars == null || chars.Length != size) return false;
            s = new String(ReadChars(size));
            return true;
        }

        /// <summary>
        /// Read an sbyte
        /// </summary>
        public sbyte ReadSByte() {
            var r = ReadNextByte();
            CheckEOF(r);
            return (sbyte)r;
        }

        /// <summary>
        /// Read an Int32
        /// </summary>
        public Int32 ReadInt32() {
            var buff = BitConverter.GetBytes((Int32)0);
            if (ReadBytes(buff, buff.Length) != buff.Length) throw new EndOfStreamException();
            return BitConverter.ToInt32(buff, 0);
        }

        /// <summary>
        /// Read an Int32
        /// </summary>
        public int Read(ref Int32 input) {
            var buff = new byte[4];
            var result = ReadBytes(buff, 4);
            if (result == 4) {
                input = BitConverter.ToInt32(buff, 0);
            }
            return result;
        }

        /// <summary>
        /// Reaad an array of bytes
        /// </summary>
        public int Read(byte[] buff, int offset, int count) {
            return ReadBytes(buff, offset, count);
        }

        /// <summary>
        /// Read an Double
        /// </summary>
        public Double ReadDouble() {
            var buff = BitConverter.GetBytes((Double)0);
            if (ReadBytes(buff, buff.Length) != buff.Length) throw new EndOfStreamException();
            return BitConverter.ToDouble(buff, 0);
        }

        /// <summary>
        /// Read an array of sbyte
        /// </summary>
        public sbyte[] ReadSBytes(int count) {
            CheckEOF();
            sbyte[] result = new sbyte[count];
            for (int i = 0; i < count; i++) {
                result[i] = ReadSByte();
            }
            return result;
        }

        /// <summary>
        /// Read an array of Int32
        /// </summary>
        public Int32[] ReadInt32s(int count) {
            Int32[] result = new Int32[count];
            for (int i = 0; i < count; i++) {
                result[i] = ReadInt32();
            }
            return result;
        }

        /// <summary>
        /// Read an array of double
        /// </summary>
        public double[] ReadDoubles(int count) {
            double[] result = new double[count];
            for (int i = 0; i < count; i++) {
                result[i] = ReadDouble();
            }
            return result;
        }

        /// <summary>
        /// True if the file is at end of file
        /// </summary>
        public bool EOF { get; private set; }

        /// <summary>
        /// Position in the file
        /// </summary>
        public long Position { get { return _Stream.Position; } }

        /// <summary>
        /// Length of the file
        /// </summary>
        public long Length { get { return _Stream.Length; } }

    }

}
