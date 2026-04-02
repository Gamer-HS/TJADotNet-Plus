namespace TJADotNet.Format
{
    /// <summary>
    /// 譜面分岐の列挙型。
    /// </summary>
    public enum Branches
    {
        /// <summary>
        /// 普通譜面
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 玄人譜面
        /// </summary>
        Expert = 1,
        /// <summary>
        /// 達人譜面
        /// </summary>
        Master = 2
    }

    public class BranchExam {
        /// <summary>
        /// 分岐条件の種類。
        /// </summary>
        public BranchType Type { get; set; }
        /// <summary>
        /// 玄人譜面へ分岐するための条件。
        /// </summary>
        public double ExpertExam { get; set; }
        /// <summary>
        /// 達人譜面へ分岐するための条件。
        /// </summary>
        public double MasterExam { get; set; }
        /// <summary>
        /// 分岐条件の範囲。
        /// </summary>
        public ExamRange Range { get; set; }
    }

    public static class BranchesConverter
    {
        public static BranchType GetBranchTypeFromString(string ch)
        {
            switch (ch)
            {
                case "p": return BranchType.Accuracy;
                case "r": return BranchType.Roll;
                case "s": return BranchType.Score;
                case "P": return BranchType.ACCURACY;
                case "jb": return BranchType.BadCount;
                default: return BranchType.Accuracy;
            }
        }

        public static ExamRange GetExamRangeFromString(string ch, BranchType bt)
        {
            switch (ch)
            {
                case "m": return ExamRange.More;
                case "l": return ExamRange.Less;
                default: return GetDefaultExamRangeFromBranchType(bt);
            }
        }

        public static ExamRange GetDefaultExamRangeFromBranchType(BranchType bt)
        {
            switch(bt)
            {
                case BranchType.Accuracy:
                case BranchType.Score:
                case BranchType.Roll:
                case BranchType.ACCURACY:
                    return ExamRange.More;

                case BranchType.BadCount:
                    return ExamRange.Less;

                default:
                    return ExamRange.More;
            }
        }
    }

    public enum BranchType {
        /// <summary>
        /// 精度
        /// </summary>
        Accuracy,
        /// <summary>
        /// 連打数
        /// </summary>
        Roll,
        /// <summary>
        /// スコア
        /// </summary>
        Score,
        /// <summary>
        /// 大音符の精度
        /// </summary>
        ACCURACY,
        /// <summary>
        /// 不可の数
        /// </summary>
        BadCount
    }

    public enum ExamRange
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
}
