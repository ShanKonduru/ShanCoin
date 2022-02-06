using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShanCoin {
    public class Blockchain : IEnumerable<IBlock> {
        private List<IBlock> _items = new List<IBlock> ();
        public Blockchain (byte[] difficulty, IBlock genesis) {
            Difficulty = difficulty;
            genesis.Hash = genesis.MineHash (difficulty);
            Items.Add (genesis);
        }

        public void Add (IBlock block) {
            if (Items.LastOrDefault () != null) {
                block.PrevHash = Items.LastOrDefault ().Hash;
            }
            block.Hash = block.MineHash (Difficulty);
            Items.Add (block);
        }

        public int Count => Items.Count;

        public IBlock this [int index] {
            get => Items[index];
            set => Items[index] = value;
        }

        public byte[] Difficulty { get; }
        public List<IBlock> Items {
            get => _items;
            set => _items = value;
        }

        IEnumerator<IBlock> IEnumerable<IBlock>.GetEnumerator () {
            return Items.GetEnumerator ();
        }

        IEnumerator IEnumerable.GetEnumerator () {
            return Items.GetEnumerator ();
        }
    }
}