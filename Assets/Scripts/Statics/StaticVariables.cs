using System.Net;
using System.Net.Sockets;

namespace UnoCardsClient.Statics
{
    public static class StaticVariables
    {
        public static Socket Client = null;
        public static IPEndPoint ClientEndPoint = null;
        public static bool GameRunning = false;
        public static string ActiveSceneName;
        public static bool MainPageRegisterButtonOnClick = false;
        public static bool MainPageLoginButtonOnClick = false;
        public static bool Registering = false;
        public static string CurrentUsername = "";
        public static string CurrentPassword = "";
        public static string RegisterStatus = null;
        public static bool RegisterStatusReceived = false;
        public static bool Logining = false;
        public static string LoginStatus = null;
        public static bool LoginStatusReceived = false;
    }
}