using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace GymHome
{
    public sealed class Settings
    {
        public class VoiceKeywords
        {
            public string MainPageSelectOption { get; set; }

            public string ExercisesPageStartExercise { get; set; }

            public string ExercisesPageNextItem { get; set; }

            public string ExercisesPagePreviousItem { get; set; }

            public string ExercisesPageSelectExercise { get; set; }

            public string ExercisesPageNextListPage { get; set; }

            public string ExercisesPagePreviousListPage { get; set; }

            public string PlanPageSelectPlan { get; set; }

            public string NavigateToPreviousPage { get; set; }

            public string NavigateToMainPage { get; set; }

            public string VideoPageEndPlan { get; set; }

            public string VideoPlay { get; set; }

            public string VideoPause { get; set; }

            public string VideoNext { get; set; }

            public string VideoPrevious { get; set; }

            public string MicrofoneMute { get; set; }

            public string MicrofoneUnmute { get; set; }

            public string MicrofoneMessageCaught { get; set; }

            //a confirm message like "yes"
            public string Confirm { get; set; }

            //a deny message like "no"
            public string Deny { get; set; }

            public void SetDefaults()
            {
                MainPageSelectOption = "selecionar_opcao";
                ExercisesPageStartExercise = "comecar";
                ExercisesPageNextItem = "proximo";
                ExercisesPagePreviousItem = "anterior";
                ExercisesPageSelectExercise = "selecionar_opcao";
                ExercisesPageNextListPage = "avancar";
                ExercisesPagePreviousListPage = "voltar";
                PlanPageSelectPlan = "selecionar_opcao";
                NavigateToPreviousPage = "voltar";
                NavigateToMainPage = "sair";
                VideoPageEndPlan = "terminar";
                MicrofoneUnmute = "listening";
                MicrofoneMute = "no_listening";
                MicrofoneMessageCaught = "legenda";
                VideoPlay = "play";
                VideoPause = "pause";
                VideoNext = "proximo";
                VideoPrevious = "anterior";
                Confirm = "confirmar";
                Deny = "negar";
            }
        }


        public VoiceKeywords voiceKeywords { get; set; }

        private static Settings m_instance;
        public static Settings Instance
        {
            get
            {
                if(m_instance == null)
                    m_instance = new Settings();

                return m_instance;
            }
        }

        public static int UserID { get; set; }

        public string ServerAddress { get; set; }

        public string MqttAddress { get; set; }

        public int MqttPort { get; set; }

        public void SetDefaults()
        {
            voiceKeywords = new VoiceKeywords();
            voiceKeywords.SetDefaults();
            ServerAddress = "http://localhost:5000";
            MqttAddress = "localhost";
            MqttPort = 1883;
        }

        public static string SettingsPath { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.xaml"); } }

        private Settings() 
        {
            SetDefaults();
        }

        public static void SaveSettings()
        {
            Settings instance = Instance;
            XmlSerializer xs = new XmlSerializer(typeof(Settings));
            TextWriter tw = new StreamWriter(SettingsPath);
            xs.Serialize(tw, instance);
            tw.Close();
        }

        public static void LoadSettings() 
        {
            m_instance = new Settings();
            XmlSerializer xs = new XmlSerializer(typeof(Settings));
            using (var sr = new StreamReader(SettingsPath)) 
            {
                m_instance = (Settings)xs.Deserialize(sr);
            }
        }

    }
}
