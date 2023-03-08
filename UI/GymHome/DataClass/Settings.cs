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

            public static void SetDefaults()
            {
                MainPageSelectOption = "selecionar_opcao";
                ExercisesPageStartExercise = "comecar";
                ExercisesPageNextItem = "proximo";
                ExercisesPagePreviousItem = "anterior";
                ExercisesPageSelectExercise = "selecionar_opcao";
                ExercisesPageNextListPage = "lista_proximo";
                ExercisesPagePreviousListPage = "lista_anterior";
                PlanPageSelectPlan = "selecionar_opcao";
                NavigateToPreviousPage = "voltar";
                NavigateToMainPage = "sair";
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
