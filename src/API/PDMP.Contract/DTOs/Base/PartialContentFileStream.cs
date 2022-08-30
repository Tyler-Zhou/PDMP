namespace PDMP.Contract.DTOs
{
    /// <summary>
    /// 文件部分内容(文件流)
    /// </summary>
    public class PartialContentFileStream : Stream
    {
        /// <summary>
        /// 起始位置
        /// </summary>
        private readonly long _start;
        /// <summary>
        /// 结束位置
        /// </summary>
        private readonly long _end;
        /// <summary>
        /// 位置
        /// </summary>
        private long _position;
        /// <summary>
        /// 文件流对象
        /// </summary>
        private FileStream _fileStream;
        /// <summary>
        /// 文件部分内容(文件流)
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public PartialContentFileStream(FileStream fileStream, long start, long end)
        {
            _start = start;
            _position = start;
            _end = end;
            _fileStream = fileStream;

            if (start > 0)
            {
                _fileStream.Seek(start, SeekOrigin.Begin);
            }
        }

        /// <summary>
        /// 将缓冲区数据写到文件
        /// </summary>
        public override void Flush()
        {
            _fileStream.Flush();
        }

        /// <summary>
        /// 设置当前下载位置
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            if (origin == SeekOrigin.Begin)
            {
                _position = _start + offset;
                return _fileStream.Seek(_start + offset, origin);
            }
            else if (origin == SeekOrigin.Current)
            {
                _position += offset;
                return _fileStream.Seek(_position + offset, origin);
            }
            else
            {
                throw new NotImplementedException("SeekOrigin.End未实现");
            }
        }

        /// <summary>
        /// 依据偏离位置读取
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            int byteCountToRead = count;
            if (_position + count > _end)
            {
                byteCountToRead = (int)(_end - _position) + 1;
            }
            var result = _fileStream.Read(buffer, offset, byteCountToRead);
            _position += byteCountToRead;
            return result;
        }
        /// <summary>
        /// 开始读取
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            int byteCountToRead = count;
            if (_position + count > _end)
            {
                byteCountToRead = (int)(_end - _position);
            }
            var result = _fileStream.BeginRead(buffer, offset,
                                               count, (s) =>
                                               {
                                                   _position += byteCountToRead;
                                                   callback(s);
                                               }, state);
            return result;
        }
        /// <summary>
        /// 设置长度
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void SetLength(long value)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 结束读取
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <returns></returns>
        public override int EndRead(IAsyncResult asyncResult)
        {
            return _fileStream.EndRead(asyncResult);
        }
        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 读取字节
        /// </summary>
        /// <returns></returns>
        public override int ReadByte()
        {
            int result = _fileStream.ReadByte();
            _position++;
            return result;
        }
        /// <summary>
        /// 写入字节
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void WriteByte(byte value)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 能否读取
        /// </summary>
        public override bool CanRead
        {
            get { return true; }
        }
        /// <summary>
        /// 能否搜索
        /// </summary>
        public override bool CanSeek
        {
            get { return true; }
        }
        /// <summary>
        /// 能否写入
        /// </summary>
        public override bool CanWrite
        {
            get { return false; }
        }
        /// <summary>
        /// 长度
        /// </summary>
        public override long Length
        {
            get { return _end - _start; }
        }
        /// <summary>
        /// 位置
        /// </summary>
        public override long Position
        {
            get { return _position; }
            set
            {
                _position = value;
                _fileStream.Seek(_position, SeekOrigin.Begin);
            }
        }
        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _fileStream.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
