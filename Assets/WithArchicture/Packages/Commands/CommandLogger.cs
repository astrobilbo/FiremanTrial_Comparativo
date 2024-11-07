using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial.WithArchitecture.Commands
{
    public static class CommandLogger
    {
        private static Dictionary<string, Command> commandList = new Dictionary<string, Command>();
        private static Dictionary<string,string> commandLogs =new Dictionary<string, string>();
        public static void LogCommand(Command command)
        {
            var logEntry = command.Log(Time.time);
            Logger(command, logEntry);
        }
        public static void LogCommand(Command command,float value)
        {
            var logEntry = command.Log(Time.time, value);
            Logger(command, logEntry);
        }

        private static void Logger(Command command , string logEntry)
        {
            var key = GenerateLogKey(logEntry, command.CommandID);
            if (commandLogs.ContainsKey(key)) return;
            commandLogs.Add(key, command.CommandID);
            Debug.Log(key);
        }
        public static void Save()
        {
            Debug.Log("Saving command logs...");
            //O save vai ser feito de maneira aditiva sem remover logs antigos
        }

        public static void Load(Dictionary<string, Command> commandRegistry)
        {
            Debug.Log("Loading command logs...");
            
            //no load o jogo pega o commando pela id e chama na ordem de acordo com o log e se o mesmo tiver um value é chamado com o value, a lista de command logs ao finalizar o load é limpa 
        }

        public static void CleanSave()
        {
            Debug.Log("Cleaning command logs...");
            
            //como os logs nao serao apagados a partir de novos saves, um botão para apagalos estara disponivel.
        }
        
        public static void RegisterCommand(Command command)
        {
            commandList.TryAdd(command.CommandID, command);
        }
        
        private static string GenerateLogKey(string logEntry, string commandID)
        {
            return $"{commandID}_{logEntry}";
        }
    }
}