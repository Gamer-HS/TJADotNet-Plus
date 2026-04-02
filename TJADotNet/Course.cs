namespace TJADotNet
{
    /// <summary>
    /// コースの列挙型。
    /// </summary>
    public enum Courses
    {
        /// <summary>
        /// かんたん
        /// </summary>
        Easy,
        /// <summary>
        /// ふつう
        /// </summary>
        Normal,
        /// <summary>
        /// むずかしい
        /// </summary>
        Hard,
        /// <summary>
        /// おに
        /// </summary>
        Oni,
        /// <summary>
        /// エディット
        /// </summary>
        Edit,
        /// <summary>
        /// 太鼓タワー
        /// </summary>
        Tower,
        /// <summary>
        /// 段位認定モード
        /// </summary>
        Dan,
        /// <summary>
        /// かんたん(裏)
        /// </summary>
        EasyEdit,
        /// <summary>
        /// ふつう(裏)
        /// </summary>
        NormalEdit,
        /// <summary>
        /// むずかしい(裏)
        /// </summary>
        HardEdit,
        /// <summary>
        /// WORLD'S END
        /// </summary>
        WE
    }

    public static class CourseConverter
    {
        /// <summary>
        /// 整数からその整数が意味するコース名を返します。該当しない場合、おにが戻り値になります。
        /// </summary>
        /// <param name="number">整数。</param>
        /// <returns>コース名。</returns>
        public static Courses GetCoursesFromNumber(int number)
        {
            switch (number)
            {
                case 0: return Courses.Easy;
                case 1: return Courses.Normal;
                case 2: return Courses.Hard;
                case 3: return Courses.Oni;
                case 4: return Courses.Edit;
                case 5: return Courses.Tower;
                case 6: return Courses.Dan;
                case 7: return Courses.EasyEdit;
                case 8: return Courses.NormalEdit;
                case 9: return Courses.HardEdit;
                case 10: return Courses.WE;
                default: return Courses.Oni;
            }
        }

        /// <summary>
        /// 文字列からその文字列が意味するコース名を返します。該当しない場合、おにが戻り値になります。
        /// </summary>
        /// <param name="str">文字列。</param>
        /// <returns>コース名。</returns>
        public static Courses GetCoursesFromString(string str)
        {
            str = str.ToLower();
            switch (str)
            {
                case "easy": return Courses.Easy;
                case "normal": return Courses.Normal;
                case "hard": return Courses.Hard;
                case "oni": return Courses.Oni;
                case "edit": return Courses.Edit;
                case "ura": return Courses.Edit;//表記揺れ
                case "oni_ex": return Courses.Edit;//表記揺れ
                case "tower": return Courses.Tower;
                case "dan": return Courses.Dan;
                case "easyedit": return Courses.EasyEdit;
                case "easy_ex":  return Courses.EasyEdit;//表記揺れ
                case "normaledit": return Courses.NormalEdit;
                case "normal_ex":  return Courses.NormalEdit;//表記揺れ
                case "hardedit": return Courses.HardEdit;
                case "hard_ex":  return Courses.HardEdit;//表記揺れ
                case "we": return Courses.WE;
                default: return Courses.Oni;
            }
        }
    }
}
