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
            List<Chip> NotesOnlyList = new List<Chip>();
            foreach (var chip in chips)
            {
                if (chip.IsNote && chip.NoteType != Notes.RollEnd && chip.NoteType != Notes.Space)
                {
                    NotesOnlyList.Add(chip);
                }

                switch (chip.NoteType)
                {
                    case Notes.DON:
                        chip.SENote = SENotes.DON;
                        break;
                    case Notes.KA:
                        chip.SENote = SENotes.KA;
                        break;
                    case Notes.RollStart:
                        chip.SENote = SENotes.RollStart;
                        break;
                    case Notes.RollEnd:
                        chip.SENote = SENotes.RollEnd;
                        break;
                    case Notes.ROLLStart:
                        chip.SENote = SENotes.ROLLStart;
                        break;
                    case Notes.Balloon:
                        chip.SENote = SENotes.Balloon;
                        break;
                    case Notes.Kusudama:
                        chip.SENote = SENotes.Kusudama;
                        break;
                }
            }

            GenerateSENotesFromNotesOnlyList(NotesOnlyList);

            GenerateKo(NotesOnlyList);
        }

        private static void GenerateSENotesFromNotesOnlyList(List<Chip> notesOnlyList)
        {
            const int DATA = 3;
            Notes[] sort = new Notes[7];
            double[] timeMicroSecond = new double[7];
            /// <summary>
            /// 16÷何分音符で隣接しているか。(ex. 2の場合は8分音符、1の場合は16分音符)
            /// </summary>
            double[] time = new double[7];
            double[] scroll = new double[7];
            double[] bpm = new double[7];
            double time_tmp;

            for (int i = 0; i < notesOnlyList.Count; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (i + (j - 3) < 0)
                    {
                        sort[j] = Notes.Space;
                        timeMicroSecond[j] = -1000000000;
                        scroll[j] = 1.0;
                        bpm[j] = notesOnlyList[0].BPM;
                    }
                    else if (i + (j - 3) >= notesOnlyList.Count)
                    {
                        sort[j] = Notes.Space;
                        timeMicroSecond[j] = 1000000000;
                        scroll[j] = 1.0;
                        bpm[j] = notesOnlyList[0].BPM;
                    }
                    else
                    {
                        sort[j] = notesOnlyList[i + (j - 3)].NoteType;
                        timeMicroSecond[j] = notesOnlyList[i + (j - 3)].Time;
                        scroll[j] = notesOnlyList[i + (j - 3)].Scroll;
                        bpm[j] = notesOnlyList[i + (j - 3)].BPM;
                    }
                }
                time_tmp = timeMicroSecond[DATA];
                for (int j = 0; j < 7; j++)
                {
                    // 240 / bpmが1小節の秒数
                    if (j == DATA)
                    {
                        time[j] = 0;
                        continue;
                    }
                    time[j] = (240 / bpm[DATA]) / ((timeMicroSecond[j] - time_tmp) / 1000000.0);
                    time[j] = Math.Abs(Math.Round(16.0 / time[j] * scroll[j] * 1000.0) / 1000.0);
                }

                switch (notesOnlyList[i].NoteType)
                {
                    case Notes.Don:
                        if (time[DATA - 1] <= 0.8)
                        {
                            notesOnlyList[i].SENote = SENotes.Do;
                            break;
                        }
                        //8分ドコドン
                        else if ((time[DATA - 2] >= 4.1 && time[DATA - 1] == 2 && time[DATA + 1] == 2 && time[DATA + 2] >= 4.1) && (sort[DATA - 1] == Notes.Don && sort[DATA + 1] == Notes.Don))
                        {
                            notesOnlyList[i - 1].SENote = SENotes.Do;
                            notesOnlyList[i].SENote = SENotes.Ko;
                            notesOnlyList[i + 1].SENote = SENotes.Don;
                            break;
                        }
                        //ドコドコドン
                        if (time[DATA - 3] >= 6 && time[DATA - 2] == 4 && time[DATA - 1] == 2 && time[DATA + 1] == 2 && time[DATA + 2] == 4 && time[DATA + 3] >= 6 && sort[DATA - 2] == Notes.Don && sort[DATA - 1] == Notes.Don && sort[DATA + 1] == Notes.Don && sort[DATA + 2] == Notes.Don)
                        {
                            notesOnlyList[i - 2].SENote = SENotes.Do;
                            notesOnlyList[i - 1].SENote = SENotes.Ko;
                            notesOnlyList[i + 0].SENote = SENotes.Do;
                            notesOnlyList[i + 1].SENote = SENotes.Ko;
                            notesOnlyList[i + 2].SENote = SENotes.Don;
                            i += 2;
                            //break;
                        }
                        //この音符と右の音符よりも右の音符とその右の音符のほうが2倍以上近いかつ右の音符が2以上離れている
                        else if (time[DATA + 1] / 2 >= time[DATA + 2] && time[DATA + 1] >= 2)
                        {
                            notesOnlyList[i].SENote = SENotes.Don;
                            break;
                        }
                        //右の音符が2以上離れている
                        else if (time[DATA + 1] > 2)
                        {
                            notesOnlyList[i].SENote = SENotes.Don;
                        }
                        //右の音符が1.4以上_左の音符が1.4以内
                        else if (time[DATA + 1] >= 1.4 && time[DATA - 1] <= 1.4)
                        {
                            notesOnlyList[i].SENote = SENotes.Don;
                        }
                        //右の音符が2以上_右右の音符が3以内
                        else if (time[DATA + 1] >= 2 && time[DATA + 2] <= 3)
                        {
                            notesOnlyList[i].SENote = SENotes.Don;
                        }
                        //右の音符が2以上_大音符
                        else if (time[DATA + 1] >= 2 && (sort[DATA + 1] == Notes.DON || sort[DATA + 1] == Notes.KA))
                        {
                            notesOnlyList[i].SENote = SENotes.Don;
                        }
                        else
                        {
                            notesOnlyList[i].SENote = SENotes.Do;
                        }
                        break;

                    case Notes.Ka:
                        if (time[DATA - 1] <= 0.8)
                        {
                            notesOnlyList[i].SENote = SENotes.Ka;
                            break;
                        }
                        //この音符と右の音符よりも右の音符とその右の音符のほうが2倍以上近いかつ右の音符が2以上離れている
                        else if (time[DATA + 1] / 2 >= time[DATA + 2] && time[DATA + 1] >= 2)
                        {
                            notesOnlyList[i].SENote = SENotes.Katsu;
                            break;
                        }
                        //右の音符が2以上離れている
                        if (time[DATA + 1] > 2)
                        {
                            notesOnlyList[i].SENote = SENotes.Katsu;
                        }
                        //右の音符が1.4以上_左の音符が1.4以内
                        else if (time[DATA + 1] >= 1.4 && time[DATA - 1] <= 1.4)
                        {
                            notesOnlyList[i].SENote = SENotes.Katsu;
                        }
                        //右の音符が2以上_右右の音符が3以内
                        else if (time[DATA + 1] >= 2 && time[DATA + 2] <= 3)
                        {
                            notesOnlyList[i].SENote = SENotes.Katsu;
                        }
                        //右の音符が2以上_大音符
                        else if (time[DATA + 1] >= 2 && (sort[DATA + 1] == Notes.DON || sort[DATA + 1] == Notes.KA))
                        {
                            notesOnlyList[i].SENote = SENotes.Katsu;
                        }
                        else
                        {
                            notesOnlyList[i].SENote = SENotes.Ka;
                        }
                        break;
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

        public static void GenerateKo(List<Chip> chips)
        {
            //TODO:このままだとド ド ド ドコドン(12分間隔から16分間隔)みたいなのもできちゃうからいつか対策する
            List<int> KoList = new List<int>();
            List<int> NotesList = new List<int>();
            bool CanChangeKo = true;

            void ResetKo()
            {
                if (NotesList.Count % 2 == 0) //ドコドドンみたいになっちゃう
                {
                    foreach (int num in KoList)
                    {
                        chips[num].SENote = SENotes.Do; //なのでリセット
                    }
                }
                KoList.Clear();
                NotesList.Clear();
                CanChangeKo = true; //セーフティロック解除
            }

            for (int i = 0; i < chips.Count; i++)
            {
                if (chips[i].NoteType != Notes.Don)
                {
                    // ドン以外だったら次へ進む。
                    ResetKo();
                    continue;
                }

                // とりあえず必要なものたち。
                var nowChip = chips[i];
                var beforeChip = GetBeforeChip(i, chips);
                var afterChip = GetAfterChip(i, chips);

                var nowTime = nowChip.Time;
                var diffBefore = nowTime - beforeChip.Time;
                var diffAfter = afterChip.Time - nowTime;
                var beforeBeat = Math.Abs(Math.Round(16.0 / ((240 / nowChip.BPM) / (diffBefore / 1000000.0)) * beforeChip.Scroll * 1000.0) / 1000.0);
                var afterBeat = Math.Abs(Math.Round(16.0 / ((240 / nowChip.BPM) / (diffAfter / 1000000.0)) * afterChip.Scroll * 1000.0) / 1000.0);

                if (beforeBeat == 1 && afterBeat == 1) //手前の音符と後の音符が共に16分間隔で隣接していたら
                {
                    if (beforeChip.SENote == SENotes.Do && (afterChip.SENote == SENotes.Do || afterChip.SENote == SENotes.Don) && CanChangeKo)
                    {//異物混入してないことは必要条件
                        nowChip.SENote = SENotes.Ko;
                        KoList.Add(i);
                        NotesList.Add(i);
                    }
                    else if (beforeChip.NoteType == Notes.Ka || afterChip.NoteType == Notes.Ka) //カッが混入
                    {
                        CanChangeKo = false; //セーフティロック
                        foreach (int num in KoList)
                        {
                            chips[num].SENote = SENotes.Do; //問答無用でリセット
                        }
                    }
                    else //前のノーツか後のノーツがコなのでそのまま
                    {
                        NotesList.Add(i);
                    }
                }
                else if (beforeBeat == 1) //手前の音符が16分間隔で隣接していたら
                {
                    NotesList.Add(i); //とりあえず追加
                }
                else if (afterBeat == 1) //後の音符が16分間隔で隣接していたら
                {
                    ResetKo(); //複合の最初ということだからリセット
                    NotesList.Add(i); //とりあえず追加
                }
                else 
                {
                    ResetKo();
                }
            }
        }
    }
}
