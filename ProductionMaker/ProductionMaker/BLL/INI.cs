using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Configuration;
using System.Collections;

namespace ProductionMaker
{
    public static class INI
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName,
           string lpKeyName, string lpString, string lpFileName);

        public static string Ler(string section, string key)
        {
            string path = Directory.GetCurrentDirectory();
            string fileNameAndPath = path + "/config.ini";

            if (System.IO.File.Exists(@fileNameAndPath) == false)
                return "";

            string[] texto = File.ReadAllLines(@fileNameAndPath);

            for (int linhas = 0; linhas < texto.Length; linhas++)
            {
                if (texto[linhas].Trim() == section && texto.Length - 1 > linhas)
                    return texto[linhas + 1].Trim().Replace(key + "=", "");
            }

            return string.Empty;

        }
        public static void Gravar(string section, string key, string text)
        {
            string path = Directory.GetCurrentDirectory();
            string fileNameAndPath = path + "/config.ini";

            WritePrivateProfileString(section, key, text, fileNameAndPath);
        }

        public static void GravaLinha(string texto)
        {
            string path = Directory.GetCurrentDirectory();
            string fileNameAndPath = path + "/config_maker.ini";

            //antes de gravar verifico se a linha já está gravada
            if (!TemLinha(texto))
            {
                StreamWriter wr = new StreamWriter(@fileNameAndPath, true);
                wr.WriteLine(texto);
                wr.Close();
            }
        }

        public static List<EnderFiles> GetArquivo()
        {
            string path = Directory.GetCurrentDirectory();
            string fileNameAndPath = path + "/config_maker.ini";

            List<EnderFiles> linhas = new List<EnderFiles>();
            linhas.Add(new EnderFiles("", ""));

            if (System.IO.File.Exists(@fileNameAndPath) == false)
                return linhas;

            StreamReader rd = new StreamReader(@fileNameAndPath);
            while (!rd.EndOfStream)
            {
                string nome = rd.ReadLine();

                linhas.Add(new EnderFiles(nome, nome));
            }

            rd.Close();
            return linhas;
        }

        private static bool TemLinha(string linha)
        {
            bool retorno = false;
            List<EnderFiles> linhas = GetArquivo();

            foreach (EnderFiles l in linhas)
            {
                if (l.Endereco == linha)
                {
                    retorno = true;
                    break;
                }
            }
            return retorno;
        }

    }
}
