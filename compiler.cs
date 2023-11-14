using System.Text.RegularExpressions;
using System;
using System.Net.Http.Headers;

namespace trantr;

public class Compiler
{
    public string doo(int[,] mem, string mode, string path)
    {
        string tr = "ok";
        switch (mode)
        {
            case "LIVE":
                bool running = true;
                while (running)
                {
                    Console.Write(">");
                    string action = Console.ReadLine()??"";

                    if (action == "EXIT")
                    {
                        running = false;
                    }
                    else
                    {
                        try{
                            execute(action, mem);
                        }catch(Exception e){
                            Console.WriteLine("ERROR");
                        }
                    }
                }
                break;

            case "ERROR":
                Console.WriteLine("ERROR");
                break;

        case "FILE":
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            List<string> allLines = new List<string>();
            while (!sr.EndOfStream)
            {
                allLines.Add(sr.ReadLine()??"");
            }
            sr.Close();
            fs.Close();

            int programCounter = 0;
            Dictionary<int, int> jumpCounts = new Dictionary<int, int>();
            while (programCounter < allLines.Count)
            {
                string line = allLines[programCounter];
                if (line.StartsWith("JUMP"))
                {
                    string[] jumpParts = line.Split(' ');
                    if (jumpParts.Length == 3)
                    {
                        int lineToJump = int.Parse(jumpParts[1]);
                        int timesToJump = int.Parse(jumpParts[2]);

                        if (!jumpCounts.ContainsKey(lineToJump) || jumpCounts[lineToJump] > 0)
                        {
                            if (!jumpCounts.ContainsKey(lineToJump))
                            {
                                jumpCounts[lineToJump] = timesToJump;
                            }

                            jumpCounts[lineToJump]--;
                            programCounter = lineToJump;
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR");
                    }
                }
                else
                {
                    if(execute(line, mem)=="ERROR"){
                        tr = "ERROR";
                    }
                }
                programCounter++;
            }
            break;
        }
        return tr;
    }

    
    string[] cut(string command)
    {
        List<string> final = new List<string>();
        string collecting = "";
        bool ignore = false;

        foreach (char mar in command)
        {
            if (mar == '[')
            {
                ignore = true;
            }

            if (ignore)
            {
                collecting += mar;
            }
            else if (mar == ' ')
            {
                if (!string.IsNullOrEmpty(collecting))
                {
                    final.Add(collecting);
                    collecting = "";
                }
            }
            else
            {
                collecting += mar;
            }
            if (mar == ']')
            {
                ignore = false;
                final.Add(collecting);
                collecting = "";
            }
        }

        if (!string.IsNullOrEmpty(collecting))
        {
            final.Add(collecting);
        }
        return final.ToArray();
    }

    
    bool checkxy(int x, int y)
    {
        string pattern = "^[0-7]$";
        Regex regex = new Regex(pattern);
        bool isMatchx = regex.IsMatch(x.ToString());
        bool isMatchy = regex.IsMatch(y.ToString());

        if (isMatchy && isMatchx)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    int exptoval(string exp, int[,] mem)
    {
        if (exp[0] == '[')
        {
            string pattern = @"^\[[0-7] [0-7]\]$";
            Regex regex = new Regex(pattern);

            bool isMatch = regex.IsMatch(exp);
            if (isMatch)
            {
                string[] cutted = exp.Substring(1, exp.Length - 2).Split(' ');
                int x = int.Parse(cutted[0]);
                int y = int.Parse(cutted[1]);

                return mem[x, y];
            }
            else
            {
                return -1;
            }
        }
        else
        {
            string pattern = "^[0-7]$";
            Regex regex = new Regex(pattern);
            bool isMatch = regex.IsMatch(exp);
            if (isMatch)
            {
                return int.Parse(exp);
            }
            else
            {
                return -1;
            }
        }
    }



    string execute(string command, int[,] mem)
    {
        string rvalue = "ok";
        string[] cutted = cut(command);

        string cmd = cutted[0];

        if (cutted.Count() < 2)
        {
            Console.WriteLine("ERROR");
            return "ERROR";
        }
        
        int x = exptoval(cutted[1], mem);
        int y = exptoval(cutted[2], mem);
        
        

        switch (cmd)
        {
            case "GET":
                if ( checkxy(x, y) && cutted.Count() == 3){
                    Console.WriteLine(mem[x,y]);
                }
                else
                {
                    Console.WriteLine("ERROR");
                    rvalue = "ERROR";
                }
                break;
            
            case "SET":
                if ( checkxy(x, y) && cutted.Count() == 4)
                {
                    int value = exptoval(cutted[3], mem);
                    mem[x, y] = value;
                }
                else
                {
                    Console.WriteLine("ERROR");
                    rvalue = "ERROR";

                }
                break;
            
            case "ADD":
                if ( checkxy(x, y) && cutted.Count() == 4)
                {
                    int value = exptoval(cutted[3], mem);
                    mem[x, y] += value;
                }
                else
                {
                    Console.WriteLine("ERROR");
                    rvalue = "ERROR";

                }
                break;
            
            case "SUB":
                if ( checkxy(x, y) && cutted.Count() == 4)
                {
                    int value = exptoval(cutted[3], mem);
                    mem[x, y] -= value;
                }
                else
                {
                    Console.WriteLine("ERROR");
                    rvalue = "ERROR";

                }
                break;
            
            case "DIV":
                if ( checkxy(x, y) && cutted.Count() == 4)
                {
                    int value = exptoval(cutted[3], mem);
                    mem[x, y] /= value;
                }
                else
                {
                    Console.WriteLine("ERROR");
                    rvalue = "ERROR";

                }
                break;
            
            case "MUL":
                if ( checkxy(x, y) && cutted.Count() == 4)
                {
                    int value = exptoval(cutted[3], mem);
                    mem[x, y] *= value;
                }
                else
                {
                    Console.WriteLine("ERROR");
                    rvalue = "ERROR";
                    

                }
                break;
            
            default:
                Console.WriteLine("ERROR");
                rvalue = "ERROR";
                break;
        }
        return rvalue;
        
    }
}