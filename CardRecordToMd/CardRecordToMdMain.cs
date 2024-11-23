using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using ThGameMgr.Ex.Data;
using ThGameMgr.Ex.Plugin;

namespace CardRecordToMd
{
    public class CardRecordToMdMain : SpellCardRecordsPluginBase
    {
        public override string Name => "Card Attack to Markdown";

        public override string Version => "0.1.0";

        public override string Developer => "珠音茉白/東方管制塔開発部";

        public override string Description => "御札戦歴を Markdown 形式でエクスポートします。";

        public override string CommandName => "御札戦歴をエクスポート(Markdown)";

        private readonly static Dictionary<string, string> _gameNameDictionary = new()
        {
            { "Th06", "東方紅魔郷" },
            { "Th07", "東方妖々夢" },
            { "Th08", "東方永夜抄" },
            { "Th09", "東方花映塚" },
            { "Th10", "東方風神録" },
            { "Th11", "東方地霊殿" },
            { "Th12", "東方星蓮船" },
            { "Th13", "東方神霊廟" },
            { "Th14", "東方輝針城" },
            { "Th15", "東方紺珠伝" },
            { "Th16", "東方天空璋" },
            { "Th17", "東方鬼形獣" },
            { "Th18", "東方虹龍洞" }
        };

        public override void Main(string gameId, ObservableCollection<SpellCardRecordData> spellCardRecords)
        {
            SaveFileDialog saveFileDialog = new()
            {
                FileName = $"{_gameNameDictionary[gameId]}御札戦歴.md",
                Filter = "Markdown ファイル|*.md|すべてのファイル|*.*"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string data = $"# {_gameNameDictionary[gameId]}御札戦歴<br>\r\n";
                if (spellCardRecords.Count > 0)
                {
                    foreach (SpellCardRecordData spellCardRecordData in spellCardRecords)
                    {
                        if (int.Parse(spellCardRecordData.TryCount) > 0)
                        {
                            data += $"### No.{spellCardRecordData.CardID} : {spellCardRecordData.CardName}\r\n取得数: {spellCardRecordData.GetCount}<br>\r\n挑戦数: {spellCardRecordData.TryCount}<br>\r\n取得率: {spellCardRecordData.Rate}<br>\r\n発動場所: {spellCardRecordData.Place}<br>\r\n術者: {spellCardRecordData.Enemy}\r\n";
                        }
                    }
                }

                StreamWriter streamWriter = new(saveFileDialog.FileName, false);
                streamWriter.Write(data);
                streamWriter.Close();
            }
        }
    }
}
