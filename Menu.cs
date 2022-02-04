using System;
using System.Text.RegularExpressions;

namespace MenuCreator
{
    static class MenuUtils
    {
        public static void WriteColor(string msg, ConsoleColor c)
        {
            string[] pieces = Regex.Split(msg, @"(\[[^\]]+\])");
            for (int i = 0; i < pieces.Length; i++)
            {
                if(pieces[i].StartsWith("[") && pieces[i].EndsWith("]"))
                {
                    Console.ForegroundColor = c;
                    pieces[i] = pieces[i].Substring(i, pieces[i].Length - 2);
                }
                Console.Write(pieces[i]);
                Console.ResetColor();
            }
        }
        public static void ClearLine(int top)
        {
            Console.SetCursorPosition(0, top);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, top);
        }
        public static int CreateMenu(string[] options, string title)
        {
            int titleStartTop = Console.CursorTop;
            
            // Scrittura del titolo
            Console.WriteLine(title);

            int menuStartTop = Console.CursorTop;
            
            // Impostazione del cursore a invisibile per estetica
            Console.CursorVisible = false;
            
            // Scrittura delle opzioni
            for (int i = 0; i < options.Length; i++)
            {
                WriteColor($"[>] {options[i]}\n", ConsoleColor.White);
            }

            int currentOption = menuStartTop;
            int oldOption = menuStartTop;

            Console.SetCursorPosition(0, currentOption);
            WriteColor("[>]", ConsoleColor.Blue);

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    // Navigazione delle opzioni
                    case ConsoleKey.UpArrow:
                        currentOption = currentOption > menuStartTop ? currentOption - 1 : menuStartTop + (options.Length - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        currentOption = currentOption < (options.Length + menuStartTop) - 1 ? 1 + ((currentOption) % (options.Length + menuStartTop)) : menuStartTop;
                        break;
                }
                Console.SetCursorPosition(0, oldOption);
                WriteColor("[>]", ConsoleColor.White);

                Console.SetCursorPosition(0, currentOption);
                WriteColor("[>]", ConsoleColor.Blue);

                oldOption = currentOption;

            } while (key.Key != ConsoleKey.Enter);
            
            // Cancellazione del menu dalla console
            for (int i = options.Length - 1; i >= titleStartTop; i--)
            {
                ClearLine(i);
            }
            // Restituzione dell'indice dell'opzione partendo da 0
            return currentOption - menuStartTop;
        }
    }
}
