using System;

namespace ShanCoin {
    public class Block : IBlock {
        public byte[] Data { get; }
        public byte[] Hash { get; set; }
        public long Nonce { get; set; }
        public byte[] PrevHash { get; set; }
        public DateTime TimeStamp { get; }

        public override string ToString () {
            return string.Format ("{0}\r\n{1}\r\n{2}\r\n{3}",
                BitConverter.ToString (Hash),
                BitConverter.ToString (PrevHash),
                Nonce,
                TimeStamp.ToString (ShanCoin.Utilities.CONSTS.DATETIME_FORMAT));
        }
    }
}