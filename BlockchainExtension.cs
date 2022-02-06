using System;
using System.IO;
using System.Security.Cryptography;
namespace ShanCoin {
    public static class BlockchainExtension {
        public static byte[] GenerateHash (this IBlock block) {
            using (SHA512 sha = new SHA512Managed ())
            using (MemoryStream st = new MemoryStream ())
            using (BinaryWriter bw = new BinaryWriter (st)) {
                bw.Write (block.Data);
                bw.Write (block.Nonce);
                bw.Write (block.TimeStamp.ToBinary ());
                bw.Write (block.PrevHash);
                var starr = st.ToArray ();
                return sha.ComputeHash (starr);
            }
        }

        public static byte[] MineHash (this IBlock block, byte[] difficulty) { }

        public static bool IsValid (this IBlock block) { }

        public static bool IsValidPrevBlock (this IBlock block, IBlock prevBlock) { }

        public static bool IsValid (this IEnumerable<IBlock> items) { }
    }
}