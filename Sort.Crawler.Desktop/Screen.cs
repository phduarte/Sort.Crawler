using System;

namespace Sort.Crawler.Desktop {
    public class Screen {

        public static void WriteLine(string texto) {
            Console.WriteLine("{0:dd/MM/yyyy HH:mm:ss} - {1}", DateTime.Now, texto);
        }

        public static void Error(string error) {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            WriteLine(error);
            Console.ForegroundColor = color;
        }

        public static void Log(string texto) {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine(texto);
            Console.ForegroundColor = color;
        }

        public static void Success(string texto) {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            WriteLine(texto);
            Console.ForegroundColor = color;
        }
    }
}
