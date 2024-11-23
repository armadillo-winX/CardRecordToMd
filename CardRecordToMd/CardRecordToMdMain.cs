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

        public override string Developer => "�쉹䝔�/�����ǐ����J����";

        public override string Description => "��D����� Markdown �`���ŃG�N�X�|�[�g���܂��B";

        public override string CommandName => "��D������G�N�X�|�[�g(Markdown)";

        private readonly static Dictionary<string, string> _gameNameDictionary = new()
        {
            { "Th06", "�����g����" },
            { "Th07", "�����d�X��" },
            { "Th08", "�����i�鏴" },
            { "Th09", "�����ԉf��" },
            { "Th10", "�������_�^" },
            { "Th11", "�����n��a" },
            { "Th12", "�������@�D" },
            { "Th13", "�����_��_" },
            { "Th14", "�����P�j��" },
            { "Th15", "��������`" },
            { "Th16", "�����V����" },
            { "Th17", "�����S�`�b" },
            { "Th18", "����������" }
        };

        public override void Main(string gameId, ObservableCollection<SpellCardRecordData> spellCardRecords)
        {
            SaveFileDialog saveFileDialog = new()
            {
                FileName = $"{_gameNameDictionary[gameId]}��D���.md",
                Filter = "Markdown �t�@�C��|*.md|���ׂẴt�@�C��|*.*"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string data = $"# {_gameNameDictionary[gameId]}��D���<br>\r\n";
                if (spellCardRecords.Count > 0)
                {
                    foreach (SpellCardRecordData spellCardRecordData in spellCardRecords)
                    {
                        if (int.Parse(spellCardRecordData.TryCount) > 0)
                        {
                            data += $"### No.{spellCardRecordData.CardID} : {spellCardRecordData.CardName}\r\n�擾��: {spellCardRecordData.GetCount}<br>\r\n���퐔: {spellCardRecordData.TryCount}<br>\r\n�擾��: {spellCardRecordData.Rate}<br>\r\n�����ꏊ: {spellCardRecordData.Place}<br>\r\n�p��: {spellCardRecordData.Enemy}\r\n";
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
