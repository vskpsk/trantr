using System;
using System.IO;
using System.Threading;

namespace trantr
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string mode;
            string path = "";
            if(args.Length > 0){ 
                if(File.Exists(args[0])){
                    mode = "FILE";
                    path = args[0];
                }else{
                    mode = "ERROR";
                }
            }else{
                mode = "LIVE";
            }
            
            int[,] mem = new int[8, 8];
            Compiler cp = new Compiler();
            
            cp.doo(mem, mode, path);
            

        }
    }
}