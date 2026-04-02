using System.Collections.Generic;

namespace TJADotNet
{
    /// <summary>
    /// コース情報クラス。
    /// </summary>
    public class CourseInfo
    {
        /// <summary>
        /// コース名。
        /// </summary>
        public Courses Course { get; set; }
        /// <summary>
        /// 難易度。
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// ふうせん連打。
        /// </summary>
        public List<int> Balloon { get; set; } = new List<int>();
        /// <summary>
        /// 普通譜面のふうせん連打。
        /// </summary>
        public List<int> BalloonNormal { get; set; } = new List<int>();
        /// <summary>
        /// 玄人譜面のふうせん連打。
        /// </summary>
        public List<int> BalloonExpert { get; set; } = new List<int>();
        /// <summary>
        /// 達人譜面のふうせん連打。
        /// </summary>
        public List<int> BalloonMaster { get; set; } = new List<int>();
        /// <summary>
        /// 譜面の作成者(難易度ごと)。
        /// </summary>
        public string NotesDesigner { get; set; }
        /// <summary>
        /// 初項。
        /// </summary>
        public int ScoreInit { get; set; }
        /// <summary>
        /// 真打配点時の初項。
        /// </summary>
        public int ScoreInit_Shinuchi { get; set; }
        /// <summary>
        /// 公差。
        /// </summary>
        public int ScoreDiff { get; set; }
        /// <summary>
        /// ニジイロ配点。
        /// </summary>
        public int ScoreNiji { get; set; }
        /// <summary>
        /// 譜面スタイル。
        /// </summary>
        public Styles Style { get; set; }
        /// <summary>
        /// 段位認定モードの条件1。
        /// </summary>
        public Exam Exam1 { get; set; }
        /// <summary>
        /// 段位認定モードの条件2。
        /// </summary>
        public Exam Exam2 { get; set; }
        /// <summary>
        /// 段位認定モードの条件3。
        /// </summary>
        public Exam Exam3 { get; set; }
        /// <summary>
        /// 段位認定モードの条件4。
        /// </summary>
        public Exam Exam4 { get; set; }
        /// <summary>
        /// 段位認定モードの条件5。
        /// </summary>
        public Exam Exam5 { get; set; }
        /// <summary>
        /// 段位認定モードの条件6。
        /// </summary>
        public Exam Exam6 { get; set; }
        /// <summary>
        /// 段位認定モードの条件7。
        /// </summary>
        public Exam Exam7 { get; set; }
        /// <summary>
        /// 段位認定モードの幕画面リスト。
        /// </summary>
        public List<NextSong> NextSongs { get; set; } = new List<NextSong>();
        /// <summary>
        /// ゲージ増加量モード。
        /// </summary>
        public Gauges GaugeIncrease { get; set; }
        /// <summary>
        /// 総音符数当たりのゲージ量。
        /// </summary>
        public double Total { get; set; }
        /// <summary>
        /// この難易度に分岐が存在するかどうか。
        /// </summary>
        public bool IsExistsBranch { get; set; } = false;
        /// <summary>
        /// 譜面分岐を分岐時まで隠す。
        /// </summary>
        public bool HiddenBranch { get; set; }
        /// <summary>
        /// ゲージの伸び率。ゲージMAXを10000点としたときの良1打の点数で表す。
        /// 値が3つの場合は順番に普通譜面、玄人譜面、達人譜面に使用する。
        /// </summary>
        public List<int> GaugeScore { get; set; } = new List<int>();
        /// <summary>
        /// WEの属性。
        /// </summary>
        public string WE_Text { get; set; }
        /// <summary>
        /// パパママモードの有無。
        /// </summary>
        public bool PapaMama { get; set; }
    }

    /// <summary>
    /// 譜面スタイル。
    /// </summary>
    public enum Styles
    {
        /// <summary>
        /// シングル譜面。
        /// </summary>
        Single,
        /// <summary>
        /// ダブル譜面。
        /// </summary>
        Double
    }

    /// <summary>
    /// 段位認定モードクラス。
    /// </summary>
    public class Exam
    {
        /// <summary>
        /// 条件の種類。
        /// </summary>
        public Conditions Condition { get; set; }
        /// <summary>
        /// 条件の範囲。
        /// </summary>
        public Scopes Scope { get; set; }
        /// <summary>
        /// 赤合格の閾値。
        /// </summary>
        public int RedValue { get; set; }
        /// <summary>
        /// 金合格の閾値。
        /// </summary>
        public int GoldValue { get; set; }
        /// <summary>
        /// 曲別条件かどうか。
        /// </summary>
        public bool IsSong { get; set; }
        /// <summary>
        /// 曲別条件だった場合の条件。
        /// </summary>
        public List<DanValue> ValueIsSong { get; set; } = new List<DanValue>(); //この辺りは適当だから気にしないで…
    }

    /// <summary>
    /// 段位認定モードの条件の種類の列挙型。
    /// </summary>
    public enum Conditions
    {
        /// <summary>
        /// ゲージ・
        /// </summary>
        Gauge,
        /// <summary>
        /// 良の数。
        /// </summary>
        JudgePerfect,
        /// <summary>
        /// 可の数。
        /// </summary>
        JudgeGood,
        /// <summary>
        /// 不可の数。
        /// </summary>
        JudgeBad,
        /// <summary>
        /// スコア。
        /// </summary>
        Score,
        /// <summary>
        /// 連打数。
        /// </summary>
        Roll,
        /// <summary>
        /// たたけた数。
        /// </summary>
        Hit,
        /// <summary>
        /// 最大コンボ数。
        /// </summary>
        Combo
    }

    /// <summary>
    /// 段位認定モードの条件の範囲の列挙型。
    /// </summary>
    public enum Scopes
    {
        /// <summary>
        /// 以上
        /// </summary>
        More,
        /// <summary>
        /// 未満
        /// </summary>
        Less
    }

    /// <summary>
    /// ゲージ増加量のモードの列挙型。
    /// </summary>
    public enum Gauges
    {
        /// <summary>
        /// 普通。シミュレータによって挙動が異なる。
        /// </summary>
        Normal,
        /// <summary>
        /// 切り捨て。
        /// </summary>
        Floor,
        /// <summary>
        /// 四捨五入。
        /// </summary>
        Round,
        /// <summary>
        /// 切り上げ。
        /// </summary>
        Ceiling,
        /// <summary>
        /// 丸め処理を行わない。
        /// </summary>
        NotFix
    }

    /// <summary>
    /// 段位認定モードの幕画面クラス。
    /// </summary>
    public class NextSong
    {
        /// <summary>
        /// タイトル。
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// サブタイトル。
        /// </summary>
        public string SubTitle { get; set; }
        /// <summary>
        /// サブタイトルの表示方式。
        /// </summary>
        public SubTitleModes SubTitleMode { get; set; }
        /// <summary>
        /// ジャンル。
        /// </summary>
        public string Genre { get; set; }
        /// <summary>
        /// 音源ファイル。
        /// </summary>
        public string Wave { get; set; }
        /// <summary>
        /// 初項。
        /// </summary>
        public int ScoreInit { get; set; }
        /// <summary>
        /// 公差。
        /// </summary>
        public int ScoreDiff { get; set; }
        /// <summary>
        /// コース名。
        /// </summary>
        public Courses Course { get; set; }
        /// <summary>
        /// 難易度。
        /// </summary>
        public int Level { get; set; }
    }

    public class DanValue
    {
        /// <summary>
        /// 赤合格の閾値。
        /// </summary>
        public int RedValue { get; set; } = 0;
        /// <summary>
        /// 金合格の閾値。
        /// </summary>
        public int GoldValue { get; set; } = 0;
    }
}
