namespace TJADotNet
{
    /// <summary>
    /// チップのクラス。
    /// </summary>
    public class Chip
    {
        /// <summary>
        /// 時間。
        /// </summary>
        public long Time { get; set; }
        /// <summary>
        /// チップのタイプ。
        /// </summary>
        public Format.Chips ChipType { get; set; }
        /// <summary>
        /// 音符のタイプ。
        /// </summary>
        public Format.Notes NoteType { get; set; }
        /// <summary>
        /// 音符のSE。
        /// </summary>
        public Format.SENotes SENote { get; set; }
        /// <summary>
        /// 何譜面か。
        /// </summary>
        public Format.Branches Branch { get; set; }
        /// <summary>
        /// 譜面分岐中の譜面かどうか。
        /// </summary>
        public bool Branching { get; set; }
        /// <summary>
        /// スクロール速度。
        /// </summary>
        public double Scroll { get; set; }
        /// <summary>
        /// BPM。
        /// </summary>
        public double BPM { get; set; }
        /// <summary>
        /// 音符の流れてくる方向(弧度法)。
        /// </summary>
        public double Direction { get; set; }
        /// <summary>
        /// 音符であるかどうか。
        /// </summary>
        public bool IsNote { get { return ChipType == Format.Chips.Note; } }
        /// <summary>
        /// このチップが叩かれたか・判定枠を通ったか。
        /// </summary>
        public bool IsHitted { get; set; }
        /// <summary>
        /// このチップが見えるかどうか。
        /// </summary>
        public bool CanShow { get; set; }
        /// <summary>
        /// このチップがゴーゴータイム中のものかどうか。
        /// </summary>
        public bool IsGoGoTime { get; set; }
        /// <summary>
        /// このチップが叩かれたとき、実際の時間とどのくらい差があったか。
        /// </summary>
        public int TimeLag { get; set; }
        /// <summary>
        /// 黄色連打数・ふうせん連打ノルマ。
        /// </summary>
        public int RollCount { get; set; }
        /// <summary>
        /// 黄色連打・ふうせん連打の叩かれた打数。
        /// </summary>
        public int HittedRollCount { get; set; }
        /// <summary>
        /// 小節数。
        /// </summary>
        public int MeasureCount { get; set; }
        /// <summary>
        /// Measure。
        /// </summary>
        public Format.Measure Measure { get; set; }
        /// <summary>
        /// 連打の終端。
        /// </summary>
        public Chip RollEnd { get; set; }
        /// <summary>
        /// SUDDENが有効かどうか。
        /// </summary>
        public bool sudden { get; set; }
        /// <summary>
        /// SUDDENの表示開始時間。
        /// </summary>
        public double suddenShowTime { get; set; }
        /// <summary>
        /// SUDDENの移動開始時間。
        /// </summary>
        public double suddenMoveTime { get; set; }
        /// <summary>
        /// 分岐の条件。
        /// </summary>
        public Format.BranchExam branchExam { get; set; } = new Format.BranchExam();
        /// <summary>
        /// 分岐開始時間。
        /// </summary>
        public long branchStartTime { get; set; }
        /// <summary>
        /// 分岐終了時間。
        /// </summary>
        public long branchEndTime { get; set; }
        /// <summary>
        /// 現在の歌詞。
        /// </summary>
        public string Lyric { get; set; }
        /// <summary>
        /// LevelRedirの効果でどの分岐に移動するか。
        /// </summary>
        public Format.Branches[] RedirectBranch { get; set; } = 
        {
            Format.Branches.Normal,
            Format.Branches.Expert,
            Format.Branches.Master
        };

        public override string ToString()
        {
            var s = IsNote
                ? string.Format("Time: {0:10} / NoteType: {1} / Branch: {2} / IsGoGoTime: {3}", Time, NoteType, Branch, IsGoGoTime)
                : string.Format("Time: {0:10} / ChipType: {1} / Branch: {2} / IsGoGoTime: {3}", Time, ChipType, Branch, IsGoGoTime);
            return s;
        }
    }
}
