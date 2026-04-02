namespace TJADotNet.Format
{
    /// <summary>
    /// 音符の列挙型。
    /// </summary>
    public enum Notes
    {
        /// <summary>
        /// 空白
        /// </summary>
        Space,
        /// <summary>
        /// ドン
        /// </summary>
        Don,
        /// <summary>
        /// カッ
        /// </summary>
        Ka,
        /// <summary>
        /// ドン(大)
        /// </summary>
        DON,
        /// <summary>
        /// カッ(大)
        /// </summary>
        KA,
        /// <summary>
        /// 連打開始
        /// </summary>
        RollStart,
        /// <summary>
        /// 連打開始(大)
        /// </summary>
        ROLLStart,
        /// <summary>
        /// ふうせん連打
        /// </summary>
        Balloon,
        /// <summary>
        /// 連打終了
        /// </summary>
        RollEnd,
        /// <summary>
        /// くすだま
        /// </summary>
        Kusudama,
        /// <summary>
        /// ドン(手)
        /// </summary>
        DONHand,
        /// <summary>
        /// カッ(手)
        /// </summary>
        KAHand,
        /// <summary>
        /// 爆弾
        /// </summary>
        Bomb,
        /// <summary>
        /// AD-LIB
        /// </summary>
        ADLIB,
        /// <summary>
        /// カドン
        /// </summary>
        Kadon,
        /// <summary>
        /// 水風船
        /// </summary>
        BlueBalloon,
        /// <summary>
        /// でんでん
        /// </summary>
        Denden
    }

    public static class NotesConverter
    {
        public static Notes GetNotesFromChar(char ch)
        {
            switch (ch)
            {
                case '0': return Notes.Space;
                case '1': return Notes.Don;
                case '2': return Notes.Ka;
                case '3': return Notes.DON;
                case '4': return Notes.KA;
                case '5': return Notes.RollStart;
                case '6': return Notes.ROLLStart;
                case '7': return Notes.Balloon;
                case '8': return Notes.RollEnd;
                case '9': return Notes.Kusudama;
                case 'A': return Notes.DONHand;
                case 'B': return Notes.KAHand;
                case 'C': return Notes.Bomb;
                case 'F': return Notes.ADLIB;
                case 'G': return Notes.Kadon;
                case 'W': return Notes.BlueBalloon;
                case 'P': return Notes.Denden;
                default: return Notes.Space;
            }
        }
    }
}
