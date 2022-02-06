using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public static byte[] MineHash (this IBlock block, byte[] difficulty) {
            if (difficulty == null) throw new ArgumentNullException (nameof (difficulty));
            byte[] hash = new byte[0];
            int difficultyLength = difficulty.Length;
            while (!hash.Take (2).SequenceEqual (difficulty)) {
                block.Nonce++;
                hash = block.GenerateHash ();
            }

            return hash;
        }

        public static bool IsValid (this IBlock block) {
            var b = block.GenerateHash ();
            return block.Hash.SequenceEqual (b);
        }

        public static bool IsValidPrevBlock (this IBlock block, IBlock prevBlock) { 
            if(prevBlock==null) throw new ArgumentNullException(nameof(prevBlock));
            var prev = prevBlock.GenerateHash();
            return prevBlock.IsValid() &  prevBlock.PrevHash.SequenceEqual(prev);
        }

        public static bool IsValid (this IEnumerable<IBlock> items) { 
            var enumerable = items.ToList();
            return enumerable.Zip(enumerable.Skip(1), Tuple.Create).All( block => block.Item2.IsValid() && block.Item2.Hash != null); 
        }
    }
}