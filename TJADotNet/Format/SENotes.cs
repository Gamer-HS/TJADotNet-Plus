using System;
using System.Collections.Generic;

namespace TJADotNet.Format
{
    /// <summary>
    /// 音符の擬音列挙型。
    /// </summary>
    public enum SENotes
    {
        /// <summary>
        /// ドン
        /// </summary>
        Don,
        /// <summary>
        /// ド
        /// </summary>
        Do,
        /// <summary>
        /// コ
        /// </summary>
        Ko,
        /// <summary>
        /// カッ
        /// </summary>
        Katsu,
        /// <summary>
        /// カ
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
        /// 連打
        /// </summary>
        RollStart,
        /// <summary>
        /// 連打(大)
        /// </summary>
        ROLLStart,
        /// <summary>
        /// 連打中
        /// </summary>
        Rolling,
        /// <summary>
        /// 連打終了
        /// </summary>
        RollEnd,
        /// <summary>
        /// ふうせん
        /// </summary>
        Balloon,
        /// <summary>
        /// くすだま
        /// </summary>
        Kusudama
    }

    public static class SENoteGenerator
    {
        /// <summary>
        /// Chip型Listから前後の譜面を考慮してSENotesを返します。
        /// </summary>
        /// <param name="chips">Chip型List</param>
        /// <returns></returns>
        public static void GenerateSENotes(List<Chip> chips)
        { //TODO:いつかTJAP3系統ベースのSENotesのジェネレータに変更する
            for (int i = 0; i < chips.Count; i++)
            {
                if (chips[i].NoteType == Notes.Space)
                {
                    // 空白だったら次へ進む。
                    continue;
                }

                // とりあえず必要なものたち。
                var nowChip = chips[i];
                var beforeChip = GetBeforeChip(i, chips);
                var afterChip = GetAfterChip(i, chips);

                // 実際にSENoteをつける。
                // 確定分はそのまま処理。

                switch (nowChip.NoteType)
                {
                    case Notes.DON:
                        nowChip.SENote = SENotes.DON;
                        break;
                    case Notes.KA:
                        nowChip.SENote = SENotes.KA;
                        break;
                    case Notes.RollStart:
                        nowChip.SENote = SENotes.RollStart;
                        break;
                    case Notes.RollEnd:
                        nowChip.SENote = SENotes.RollEnd;
                        break;
                    case Notes.ROLLStart:
                        nowChip.SENote = SENotes.ROLLStart;
                        break;
                    case Notes.Balloon:
                        nowChip.SENote = SENotes.Balloon;
                        break;
                    case Notes.Kusudama:
                        nowChip.SENote = SENotes.Kusudama;
                        break;
                    default:
                        nowChip.SENote = GetSENoteFromDuration(i, chips);
                        break;
                }
            }
        }

        public static void GenerateKo(List<Chip> chips)
        {
            List<int> KoList = new List<int>();
            List<int> List = new List<int>();
            bool IsChangeKo = true;
            for (int i = 0; i < chips.Count; i++)
            {
                if (chips[i].NoteType != Notes.Don)
                {
                    // ドン以外だったら次へ進む。
                    continue;
                }

                // とりあえず必要なものたち。
                var nowChip = chips[i];
                var beforeChip = GetBeforeChip(i, chips);
                var afterChip = GetAfterChip(i, chips);

                var nowTime = nowChip.Time;
                var diffBefore = nowTime - beforeChip.Time;
                var diffAfter = afterChip.Time - nowTime;

                //これ以降、「nowChip.Measure.GetRate()」は「240」に置き換えられています。悪しからず

                long time16;
                if (nowChip.BPM > 0)
                {
                    time16 = (long)(240 / (nowChip.BPM * nowChip.Scroll) / 16 * 1000 * 1000.0); //見た目BPMの話なのでスクロール値も乗算する
                }
                else
                {
                    time16 = (long)(240 / (60 * nowChip.Scroll) / 16 * 1000 * 1000.0);
                }

                if (Math.Abs(diffBefore - time16) < 10 && Math.Abs(diffAfter - time16) < 10)
                {
                    if(beforeChip.SENote == SENotes.Do && (afterChip.SENote == SENotes.Do || afterChip.SENote == SENotes.Don) && IsChangeKo)
                    {
                        nowChip.SENote = SENotes.Ko;
                        KoList.Add(i);
                        List.Add(i);
                    }
                    else if(beforeChip.NoteType == Notes.Ka || afterChip.NoteType == Notes.Ka) 
                    {
                        IsChangeKo = false;
                        foreach(int num in KoList) {
                            chips[num].SENote = SENotes.Do;
                        }
                    }
                    else
                    {
                        List.Add(i);
                    }
                }
                else if(Math.Abs(diffBefore - time16) < 10 || Math.Abs(diffAfter - time16) < 10)
                {
                    List.Add(i);
                }
                else {
                    if (List.Count % 2 == 0)
                    {
                        foreach (int num in KoList)
                        {
                            chips[num].SENote = SENotes.Do;
                        }
                    }
                    KoList.Clear();
                    List.Clear();
                    IsChangeKo = true;
                }
            }
        }
        private static Chip GetBeforeChip(int i, IReadOnlyList<Chip> chips)
        {
            // 範囲を超えない範囲(?)で、前後のチップを取得する。
            if (i > 0)
            {
                // 1以上なので必ず前のチップがある
                for (int index = i - 1; index > 0; index--)
                {
                    if (chips[index].NoteType != Notes.Space)
                    {
                        // 空白以外の音符があるなら、それを記憶。
                        return chips[index];
                    }
                }
            }
            return chips[0];
        }

        private static Chip GetAfterChip(int i, IReadOnlyList<Chip> chips)
        {
            if (i < chips.Count - 2)
            {
                // Listの最大値の-1以下なので必ず後のチップがある
                for (int index = i + 1; index < chips.Count; index++)
                {
                    if (chips[index].NoteType != Notes.Space)
                    {
                        // 空白以外の音符があるなら、それを記憶。
                        return chips[index];
                    }
                }
            }
            return chips[chips.Count - 1];
        }

        private static SENotes GetSENoteFromDuration(int i, IReadOnlyList<Chip> chips)
        {
            // とりあえず必要なものたち。
            var nowChip = chips[i];
            var beforeChip = GetBeforeChip(i, chips);
            var afterChip = GetAfterChip(i, chips);

            var nowTime = nowChip.Time;
            var diffBefore = nowTime - beforeChip.Time;
            var diffAfter = afterChip.Time - nowTime;
            var diffBeforeS = nowTime - beforeChip.Time;
            var diffAfterS = afterChip.Time - nowTime;

            long time16S = (long)(240 / (nowChip.BPM * nowChip.Scroll) / 16 * 1000 * 1000.0);
            long time12S = (long)(240 / (nowChip.BPM * nowChip.Scroll) / 12 * 1000 * 1000.0);
            long time8S = (long)(240 / (nowChip.BPM * nowChip.Scroll) / 8 * 1000 * 1000.0);
            long time6S = (long)(240 / (nowChip.BPM * nowChip.Scroll) / 6 * 1000 * 1000.0);

            /*
            if (diffBefore < time16S || diffAfter < time8S)
            {
                if (nowChip.NoteType == Notes.Don) return SENotes.Do;
                if (nowChip.NoteType == Notes.Ka) return SENotes.Ka;
            }
            else if (diffAfter >= 4.0 / 3 * diffBefore || diffAfter <= 2.0 / 3 * diffBefore)
            {
                if (nowChip.NoteType == Notes.Don) return SENotes.Don;
                if (nowChip.NoteType == Notes.Ka) return SENotes.Katsu;
            }*/

            long time16;
            long time12;
            long time8;

            time16 = (long)(240 / nowChip.BPM / 16 * 1000 * 1000.0);
            time12 = (long)(240 / nowChip.BPM / 12 * 1000 * 1000.0);
            time8 = (long)(240 / nowChip.BPM / 8 * 1000 * 1000.0);

            long time17S = (long)(240 / (nowChip.BPM * nowChip.Scroll) / 17 * 1000 * 1000.0);


            if (diffBefore <= time17S)//前の音符との見た目隣接間隔が17分以上だったら
            {
                if (nowChip.NoteType == Notes.Don) return SENotes.Do;
                if (nowChip.NoteType == Notes.Ka) return SENotes.Ka;
            }
            
            else if(diffAfter > time6S)//後の音符との見た目隣接感覚が6分未満だったら
            {
                if (nowChip.NoteType == Notes.Don) return SENotes.Don;
                if (nowChip.NoteType == Notes.Ka) return SENotes.Katsu;
            }
            else if(diffAfter <= time16S)//前の音符との見た目隣接間隔が16分以上だったら
            {
                if (nowChip.NoteType == Notes.Don) return SENotes.Do;
                if (nowChip.NoteType == Notes.Ka) return SENotes.Ka;
            }
            else if(diffBefore * 1.5 >= diffAfter)//前の音符との隣接間隔の1.5倍が後の音符との隣接間隔以上だったら
            {
                if (nowChip.NoteType == Notes.Don) return SENotes.Do;
                if (nowChip.NoteType == Notes.Ka) return SENotes.Ka;
            }
            else if(diffBefore * 1.5 < diffAfter)//後の音符との隣接間隔が前の音符との隣接間隔の1.5倍を超えていたら
            {
                if (nowChip.NoteType == Notes.Don) return SENotes.Don;
                if (nowChip.NoteType == Notes.Ka) return SENotes.Katsu;
            }
            /*
            if (diffBefore > time12 && diffAfter > time12)
            {
                // 3連符の間隔より大きく離れてる …… ドン・カッ
                if (nowChip.NoteType == Notes.Don) return SENotes.Don;
                if (nowChip.NoteType == Notes.Ka) return SENotes.Katsu;
            }

            if (diffBefore <= time12 && diffAfter <= time12)
            {
                // 3連符の間隔未満離れてる …… ド・カ
                if (nowChip.NoteType == Notes.Don) return SENotes.Do;
                if (nowChip.NoteType == Notes.Ka) return SENotes.Ka;
            }
            
            */
            return nowChip.SENote; //最悪の場合はそのままで
        }
    }
}
