using System.Linq;
using System.Collections.Generic;
using System;
namespace ShanCoin {
    public class Program {
        static void Main (string[] args) {
            Random rnd = new Random(DateTime.Now.Millisecond);
            IBlock genesis =  new Block(new byte[]{0x00, 0x00, 0x00, 0x00, 0x00 });
            byte[] difficulty = new byte[]{0x00, 0x00};

            Blockchain chain = new Blockchain(difficulty, genesis);
            for(int i=0; i<20; i++){
                var data = Enumerable.Range(0, 2256).Select(p=> (byte) rnd.Next());
                chain.Add(new Block(data.ToArray()));
                Console.WriteLine(chain.LastOrDefault().ToString());
                Console.WriteLine($"Chain is Valid :{chain.IsValid()}");
            }

            Console.ReadLine();
        }
    }
}