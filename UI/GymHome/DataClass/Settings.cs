using System.Text.Json.Serialization;

namespace GymHome
{
    public sealed class Settings
    {
        public class VoiceKeywords
        {
            [JsonInclude]
            public static string MainPageSelectOption { get; private set; }

            [JsonInclude]
            public static string ExercisesPageStartExercise { get; private set; }

            [JsonInclude]
            public static string ExercisesPageNextItem { get; private set; }

            [JsonInclude]
            public static string ExercisesPagePreviousItem { get; private set; }

            [JsonInclude]
            public static string ExercisesPageSelectExercise { get; private set; }

            [JsonInclude]
            public static string ExercisesPageNextListPage { get; private set; }

            [JsonInclude]
            public static string ExercisesPagePreviousListPage { get; private set; }

            [JsonInclude]
            public static string PlanPageSelectPlan { get; private set; }

            [JsonInclude]
            public static string NavigateToPreviousPage { get; private set; }

            [JsonInclude]
            public static string NavigateToMainPage { get; private set; }

            public static string VideoPageEndPlan { get; private set; }

            public static string VideoPlay { get; private set; }

            public static string VideoPause { get; private set; }

            public static string VideoNext { get; private set; }

            public static string VideoPrevious { get; private set; }

            public static string MicrofoneMute { get; private set; }

            public static string MicrofoneUnmute { get; private set; }

            public static string MicrofoneMessageCaught { get; private set; }

            //a confirm message like "yes"
            public static string Confirm { get; private set; }

            //a deny message like "no"
            public static string Deny { get; private set; }

            public static void SetDefaults()
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
            //[JsonConstructor]
            //public VoiceKeywords(string mainPageSelectOption,string exercisesPageStartExercie,string exercisesPageNextItem,string exercisesPagePreviousItem,string exercisesPageSelectExercise
            //    ,string planPageSelectPlan,string navigateToPreviousPage)
            //{

            //}
        }

        public VoiceKeywords voiceKeyword { get; set; }

        public static int UserID { get; set; }

        public static void SetDefaults()
        {
            VoiceKeywords.SetDefaults();
        }

    }
}
